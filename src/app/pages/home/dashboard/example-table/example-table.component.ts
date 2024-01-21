import { Component } from '@angular/core'
import {
  NzTableFilterFn,
  NzTableFilterList,
  NzTableModule,
  NzTableSortFn,
  NzTableSortOrder,
} from 'ng-zorro-antd/table'
import { CommonModule } from '@angular/common'
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header'
import { NzSpaceModule } from 'ng-zorro-antd/space'
import { NzButtonModule } from 'ng-zorro-antd/button'
import { NzIconModule } from 'ng-zorro-antd/icon'
import { NzBadgeModule } from 'ng-zorro-antd/badge'

interface DataItem {
  id: number
  name: string
  age: number
  address: string
}

interface ColumnItem {
  name: string
  sortOrder: NzTableSortOrder | null
  sortFn: NzTableSortFn<DataItem> | null
  listOfFilter: NzTableFilterList
  filterFn: NzTableFilterFn<DataItem> | null
  filterMultiple: boolean
  sortDirections: NzTableSortOrder[]
}

@Component({
  selector: 'app-example-table',
  standalone: true,
  imports: [
    CommonModule,
    NzTableModule,
    NzPageHeaderModule,
    NzButtonModule,
    NzIconModule,
    NzBadgeModule,
  ],
  templateUrl: './example-table.component.html',
  styleUrl: './example-table.component.less',
})
export class ExampleTableComponent {
  listOfColumns: ColumnItem[] = [
    {
      name: 'Name',
      sortOrder: null,
      sortFn: (a: DataItem, b: DataItem) => a.name.localeCompare(b.name),
      sortDirections: ['ascend', 'descend', null],
      filterMultiple: true,
      listOfFilter: [
        { text: 'Joe', value: 'Joe' },
        { text: 'Jim', value: 'Jim' },
      ],
      filterFn: (list: string[], item: DataItem) =>
        list.some(name => item.name.indexOf(name) !== -1),
    },
    {
      name: 'Age',
      sortOrder: 'descend',
      sortFn: (a: DataItem, b: DataItem) => a.age - b.age,
      sortDirections: ['descend', null],
      listOfFilter: [],
      filterFn: null,
      filterMultiple: true,
    },
    {
      name: 'Address',
      sortOrder: null,
      sortDirections: ['ascend', 'descend', null],
      sortFn: (a: DataItem, b: DataItem) => a.address.length - b.address.length,
      filterMultiple: false,
      listOfFilter: [
        { text: 'London', value: 'London' },
        { text: 'Sidney', value: 'Sidney' },
      ],
      filterFn: (address: string, item: DataItem) => item.address.indexOf(address) !== -1,
    },
  ]
  listOfData: DataItem[] = [
    {
      id: 1,
      name: 'John Brown',
      age: 32,
      address: 'New York No. 1 Lake Park',
    },
    {
      id: 2,
      name: 'Jim Green',
      age: 42,
      address: 'London No. 1 Lake Park',
    },
    {
      id: 3,
      name: 'Joe Black',
      age: 32,
      address: 'Sidney No. 1 Lake Park',
    },
    {
      id: 4,
      name: 'Jim Red',
      age: 32,
      address: 'London No. 2 Lake Park',
    },
    {
      id: 5,
      name: 'Jim Pink',
      age: 32,
      address: 'London No. 2 Lake Park',
    },
  ]
}
