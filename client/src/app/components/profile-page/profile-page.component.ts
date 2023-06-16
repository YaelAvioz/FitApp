import { Component } from '@angular/core';
import { SessionService } from 'src/app/service/sessionService';
import { FoodHistory, Grade, User } from 'src/interfaces/user';
import * as moment from 'moment';
import { Mentor } from 'src/interfaces/mentor';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { loadMentorByName } from 'src/app/store/mentors-page/mentorsPageAction';
import { MentorsPageState } from 'src/app/store/mentors-page/mentorPageReducer';
import { UserState } from 'src/app/store/user/userReducer';
import { loadNutritionalValues, loadUserByUsername, loadUserFoodHistory, loadUserGrade, loadUserWater, updateUserGoal, updateUserWeight } from 'src/app/store/user/userAction';
import { FoodItem } from 'src/interfaces/foodItem';
import { Sort } from '@angular/material/sort';
import { state } from '@angular/animations';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent {
  mentor$: Observable<Mentor | null>;
  mentor !: Mentor;
  user$: Observable<User>;
  user: User;
  nutritionalValues$: Observable<FoodItem>;
  nutritionalValues !: FoodItem;
  foodHistory$: Observable<FoodHistory[]>;
  foodHistory !: FoodHistory[];
  water$: Observable<boolean[]>;
  grade$: Observable<Grade>;
  grade!: Grade;
  currentWeight!: number;
  chart: any;
  dataPoints!: any[];
  chartOptions: any;
  nutritionalValuesChart: any;
  recommendedvsActual: any;
  output!: any;
  weights: number[] = [];
  selectedGoal!: string;
  selectedWeight!: number;
  errorMessage!: string;
  successMessage!: string;
  filteredFoodItems!: FoodItem[];
  sortedData!: FoodItem[];
  water: boolean[] =[];

  constructor(private sessionService: SessionService, private store: Store<{ mentorPageReducer: MentorsPageState, userReducer: UserState }>) {
    this.mentor$ = this.store.select((state) => {
      return state.mentorPageReducer.mentor;
    })

    this.user$ = this.store.select((state) => {
      return state.userReducer.user;
    })

    this.nutritionalValues$ = this.store.select((state) => {
      return state.userReducer.nutritionalValues;
    })

    this.foodHistory$ = this.store.select((state) => {
      return state.userReducer.foodHistory;
    })

    this.grade$ = this.store.select((state) => {
      return state.userReducer.grade;
    })

    this.water$ = this.store.select((state) => {
      return state.userReducer.water;
    })

    for (let i = 40; i <= 200; i++) {
      this.weights.push(i);
    }

    this.user = this.sessionService.getUserFromSession();
    this.currentWeight = this.user.weight[this.user.weight.length - 1].item1;

    this.initializeWeightChart(this.user);
  }

  ngOnInit() {
    this.user = this.sessionService.getUserFromSession();
    this.store.dispatch(loadUserWater({ username: this.user.username }));
    this.water$.subscribe((data: boolean[]) => {
      this.water = data;
      console.log(data);
      
    });    
    this.store.dispatch(loadMentorByName({ name: this.user.mentor }));
    this.store.dispatch(loadNutritionalValues({ userId: this.user.username }));
    this.nutritionalValues$.subscribe(nutritionalValues => {
      this.nutritionalValues = nutritionalValues;
      this.initializeNutritionalValuesChart(nutritionalValues);
      this.store.dispatch(loadUserGrade({ username: this.user.username }));
      this.grade$.subscribe(grade => {
        this.grade = grade;
        this.initializeRecommendedvsActualChart(this.grade, this.nutritionalValues)
      })
    });

    this.store.dispatch(loadUserByUsername({ username: this.user.username }));
    this.store.dispatch(loadUserFoodHistory({ username: this.user.username }));
    this.foodHistory$.subscribe(foodHistoryList => {
      this.foodHistory = foodHistoryList;
      this.filteredFoodItems = getFoodHistory(this.foodHistory);
      this.sortedData = this.filteredFoodItems.slice();
    })
  }

  toggleWaterStatus(index: number) {
    let waterCopy = [...this.water]; 
    waterCopy[index] = !waterCopy[index]; 
    this.water = waterCopy; 
  }

  enterGoal() {
    if (this.selectedGoal) {
      this.errorMessage = "";
      this.successMessage = "We got your goal";
      this.store.dispatch(updateUserGoal({ userId: this.user.id, goal: this.selectedGoal }));
    }
    else {
      this.successMessage = "";
      this.errorMessage = "Please enter your goal";
    }
  }

  updateWeight() {
    if (this.selectedWeight) {
      this.errorMessage = "";
      this.successMessage = "We update your weight";
      this.store.dispatch(updateUserWeight({ userId: this.user.id, newWeight: this.selectedWeight }));
      this.user$.subscribe(currentUser => {
        this.initializeWeightChart(currentUser);
        this.user = currentUser;
      });
    }
    else {
      this.successMessage = "";
      this.errorMessage = "Please enter your weight";
    }
  }

  initializeRecommendedvsActualChart(diffrent: Grade, real: FoodItem) {
    let carbsPerDay = parseFloat(real.carbohydrate) + diffrent.carbohydrate_diff;
    let fatPerDay = parseFloat(real.fat) + diffrent.fat_diff;
    let proteinsPerDay = parseFloat(real.protein) + diffrent.protein_diff;
    let sugersPerDay = parseFloat(real.sugars) + diffrent.sugars_diff;
    let fiberPerDay = parseFloat(real.fiber) + diffrent.fiber_diff;
    let calciumPerDay = (parseFloat(real.calcium) + diffrent.calcium_diff) / 1000;

    this.recommendedvsActual = {
      animationEnabled: true,
      theme: "light2",
      title: {
        text: "Recommended vs Actual Food Consumption"
      },
      axisX: {
        labelAngle: -90
      },
      axisY: {
        title: "billion of barrels",
        minimum: 0
      },
      toolTip: {
        shared: true
      },
      legend: {
        cursor: "pointer",
        itemclick: function (e: any) {
          if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
            e.dataSeries.visible = false;
          }
          else {
            e.dataSeries.visible = true;
          }
          e.chart.render();
        }
      },
      data: [{
        type: "column",
        name: "Recommended Amount Per Day (g)",
        legendText: "Recommended Amount Per Day (g)",
        showInLegend: true,
        dataPoints: [
          { label: "Carbs", y: carbsPerDay },
          { label: "Fat", y: fatPerDay },
          { label: "Proteins", y: proteinsPerDay },
          { label: "sugers", y: sugersPerDay },
          { label: "Fiber", y: fiberPerDay },
          { label: "calcium", y: calciumPerDay },
        ]
      }, {
        type: "column",
        name: "Amount Per Day (g)",
        legendText: "Amount Per Day (g)",
        axisYType: "secondary",
        showInLegend: true,
        dataPoints: [
          { label: "Carbs", y: parseFloat(real.carbohydrate) },
          { label: "Fat", y: parseFloat(real.fat) },
          { label: "Proteins", y: parseFloat(real.protein) },
          { label: "sugers", y: parseFloat(real.sugars) },
          { label: "Fiber", y: parseFloat(real.fiber) },
          { label: "calcium", y: parseFloat(real.calcium) / 1000 },
        ]
      }]
    }
  }

  initializeNutritionalValuesChart(nutritionalValues: FoodItem) {
    this.nutritionalValuesChart = {
      animationEnabled: true,
      theme: "light2",
      creditText: "",
      creditHref: null,
      exportEnabled: false,
      title: {
        text: "Total Nutritional Values"
      },
      subtitles: [{
        text: `Daily Nutritional Intake - Total Calories ${nutritionalValues.calories}`,
      }],
      data: [{
        type: "doughnut",
        indexLabel: "{name}: {y}g",
        dataPoints: [
          { name: "Carbs", y: nutritionalValues.carbohydrate },
          { name: "Fat", y: nutritionalValues.fat },
          { name: "Proteins", y: nutritionalValues.protein },
          { name: "sugers", y: nutritionalValues.sugars },
          { name: "fiber", y: nutritionalValues.fiber },
          { name: "calcium", y: parseFloat(nutritionalValues.calcium) / 1000 },
        ]
      }]
    }
  }

  initializeWeightChart(user: User) {
    this.dataPoints = user.weight.map((item) => ({
      x: new Date(item.item2),
      y: item.item1
    }));

    this.dataPoints.forEach((dataPoint: any) => {
      const formattedDate = moment(dataPoint.x).format("MMM DD, YYYY");
      dataPoint.x = formattedDate;
    });

    this.output = this.dataPoints.map(item => ({
      x: new Date(moment(item.x, 'MMM DD, YYYY').format()),
      y: item.y
    }));

    this.chartOptions = {
      animationEnabled: true,
      creditText: "",
      creditHref: null,
      theme: "light2",
      title: {
        text: "Weight"
      },
      axisX: {
        valueFormatString: "MMM DD, YYYY"
      },
      axisY: {
        title: "Weight(kg)"
      },
      toolTip: {
        shared: true
      },
      legend: {
        cursor: "pointer",
        itemclick: function (e: any) {
          if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
            e.dataSeries.visible = false;
          } else {
            e.dataSeries.visible = true;
          }
          e.chart.render();
        }
      },
      data: [{
        type: "line",
        showInLegend: true,
        name: "Weight",
        xValueFormatString: "MMM DD, YYYY",
        dataPoints: this.output,
      }]
    };
  }

  sortData(sort: Sort) {
    const data = this.filteredFoodItems.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedData = data;
      return;
    }

    this.sortedData = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'name':
          return compare(a.name, b.name, isAsc);
        case 'calories':
          return compare(a.calories, b.calories, isAsc);
        case 'fat':
          return compare(a.fat, b.fat, isAsc);
        case 'carbs':
          return compare(a.carbohydrate, b.carbohydrate, isAsc);
        case 'protein':
          return compare(a.protein, b.protein, isAsc);
        case 'sugers':
          return compare(a.sugars, b.sugars, isAsc);
        case 'calcium':
          return compare(a.calcium, b.calcium, isAsc);
        case 'fiber':
          return compare(a.fiber, b.fiber, isAsc);
        default:
          return 0;
      }
    });
  }
}

function getFoodHistory(foodHistory: FoodHistory[]) {
  const currentDate = new Date();
  currentDate.setHours(0, 0, 0, 0); // Reset time to midnight

  const filteredItems = foodHistory.filter(item => {
    const item2 = new Date(item.item2);
    return item2.setHours(0, 0, 0, 0) === currentDate.getTime();
  }).map(item => item.item1);

  return filteredItems;
}

function compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}