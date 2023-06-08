import { Component, OnInit } from '@angular/core';
import { SessionService } from 'src/app/service/sessionService';
import { User } from 'src/interfaces/user';
import * as moment from 'moment';
import { Mentor } from 'src/interfaces/mentor';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { loadMentorByName } from 'src/app/store/mentors-page/mentorsPageAction';
import { MentorsPageState } from 'src/app/store/mentors-page/mentorPageReducer';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent {
  mentor$: Observable<Mentor | null>;
  user!: User;
  currentWeight!: number;
  chart: any;
  dataPoints!: any[];
  chartOptions: any; // Declare the chartOptions property
  output!: any;
  mentor !: Mentor;
  

  constructor(private sessionService: SessionService,private store: Store<{mentorPageReducer: MentorsPageState}>) {
    this.mentor$ = this.store.select((state) => {    
      return state.mentorPageReducer.mentor;
    })
    
    this.user = this.sessionService.getUserFromSession();
    this.currentWeight = this.user.weight[this.user.weight.length - 1].item1;
    console.log(this.user);

    this.dataPoints = this.user.weight.map((item) => ({
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

    this.initializeWeightChart();
  }

  ngOnInit(){
    this.store.dispatch(loadMentorByName({name: this.user.mentor}));
  }

  chartOptions2= {
	  animationEnabled: true,
	  theme: "light2",
	  exportEnabled: false,
	  title: {
		text: "Developer Work Week"
	  },
	  subtitles: [{
		text: "Median hours/week"
	  }],
	  data: [{
		type: "pie", //change type to column, line, area, doughnut, etc
		indexLabel: "{name}: {y}%",
		dataPoints: [
			{ name: "Carbs", y: 9.1 },
			{ name: "Problem Solving", y: 3.7 },
			{ name: "Debugging", y: 36.4 },
			{ name: "Writing Code", y: 30.7 },
			{ name: "Firefighting", y: 20.1 }
		]
	  }]
	}

  initializeWeightChart() {
    this.chartOptions = {
      animationEnabled: true,
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
