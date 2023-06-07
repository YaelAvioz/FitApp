import { Component, OnInit } from '@angular/core';
import { SessionService } from 'src/app/service/sessionService';
import { User } from 'src/interfaces/user';
import * as moment from 'moment';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent {
  user!: User;
  currentWeight!: number;
  chart: any;
  dataPoints!: any[];
  chartOptions: any; // Declare the chartOptions property
  output!: any;

  constructor(private sessionService: SessionService) {
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

    console.log("output", this.output);

    this.initializeChart();
  }

  initializeChart() {

    console.log("in", this.dataPoints);
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
