import { Component, OnInit } from '@angular/core'
import { NzIconModule } from 'ng-zorro-antd/icon'
import { Chart } from 'chart.js/auto'
import { ExampleTableComponent } from '@/app/pages/home/dashboard/example-table/example-table.component'

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [NzIconModule, ExampleTableComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.less',
})
export class DashboardComponent implements OnInit {
  chart: Chart

  ngOnInit() {
    //@ts-ignore
    this.chart = new Chart('PowerChart', {
      type: 'doughnut',
      data: {
        labels: ['Solar (DC) MW 1766', 'Storage (DC) MW 142'],
        datasets: [
          {
            data: [1766, 142],
            backgroundColor: ['rgb(245, 197, 66)', 'rgb(132, 53, 212)'],
          },
        ],
      },
      options: {
        plugins: {
          legend: {
            position: 'bottom',
            fullSize: true,
            labels: {
              font: {
                size: 18,
              },
            },
          },
          title: {
            display: true,
            text: '1,909 MW TOTAL',
            font: {
              size: 24,
            },
            position: 'bottom',
            align: 'center',
          },
        },
      },
    })
  }
}
