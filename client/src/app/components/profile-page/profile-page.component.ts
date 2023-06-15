import { Component, OnInit } from '@angular/core';
import { SessionService } from 'src/app/service/sessionService';
import { User } from 'src/interfaces/user';
import * as moment from 'moment';
import { Mentor } from 'src/interfaces/mentor';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { loadMentorByName } from 'src/app/store/mentors-page/mentorsPageAction';
import { MentorsPageState } from 'src/app/store/mentors-page/mentorPageReducer';
import { UserState } from 'src/app/store/user/userReducer';
import { loadNutritionalValues, loadUserByUsername, updateUserGoal, updateUserWeight } from 'src/app/store/user/userAction';
import { FoodItem } from 'src/interfaces/foodItem';

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
  currentWeight!: number;
  chart: any;
  dataPoints!: any[];
  chartOptions: any;
  nutritionalValuesChart: any;
  output!: any;
  weights: number[] = [];
  selectedGoal!: string;
  selectedWeight!: number;
  errorMessage!: string;
  successMessage!: string;
 
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

    for (let i = 40; i <= 200; i++) {
      this.weights.push(i);
    }

    this.user = this.sessionService.getUserFromSession();
    this.currentWeight = this.user.weight[this.user.weight.length - 1].item1;

    this.initializeWeightChart(this.user);
  }

  ngOnInit() {
    this.user = this.sessionService.getUserFromSession();
    this.store.dispatch(loadMentorByName({ name: this.user.mentor }));
    this.store.dispatch(loadNutritionalValues({ userId: this.user.username }));
    this.nutritionalValues$.subscribe(nutritionalValues => {
      this.nutritionalValues = nutritionalValues;
      this.nutritionalValuesChart = {
        animationEnabled: true,
        theme: "light2",
        creditText: "",
        creditHref: null,
        exportEnabled: false,
        title: {
          text: "Nutritional Values"
        },
        subtitles: [{
          text: `Daily Nutritional Intake - Total Calories ${nutritionalValues.calories}`,
        }],
        data: [{
          type: "pie",
          indexLabel: "{name}: {y}%",
          dataPoints: [
            { name: "Carbs", y: nutritionalValues.carbohydrate },
            { name: "Fat", y: nutritionalValues.fat },
            { name: "Proteins", y: nutritionalValues.protein },
          ]
        }]
      }
    });

    this.store.dispatch(loadUserByUsername({ username: this.user.username }));
    this.user$.subscribe(currentUser => {
      this.user = currentUser;
    });
  }

  enterGoal(){
    if(this.selectedGoal)
    {
      this.errorMessage="";
      this.successMessage="We got your goal";
      this.store.dispatch(updateUserGoal({userId: this.user.id, goal: this.selectedGoal}));
    }
    else{
      this.successMessage="";
      this.errorMessage="Please enter your goal";
    } 
  }

  updateWeight(){
    if(this.selectedWeight)
    {
      this.errorMessage="";
      this.successMessage="We update your weight";
      this.store.dispatch(updateUserWeight({userId: this.user.id, newWeight: this.selectedWeight}));
      this.user$.subscribe(currentUser => {
        this.initializeWeightChart(currentUser);      
        this.user = currentUser;
      });
      
      
    }
    else{
      this.successMessage="";
      this.errorMessage="Please enter your weight";
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
}