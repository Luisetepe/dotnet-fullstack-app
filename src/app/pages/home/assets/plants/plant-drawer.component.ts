import { CommonModule } from '@angular/common'
import {
	Component,
	EventEmitter,
	Input,
	OnInit,
	Output,
	computed,
	effect,
	inject,
	input,
	signal
} from '@angular/core'
import {
	FormBuilder,
	FormControl,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators
} from '@angular/forms'
import { NzBadgeModule } from 'ng-zorro-antd/badge'
import { NzButtonModule } from 'ng-zorro-antd/button'
import { NzDrawerModule } from 'ng-zorro-antd/drawer'
import { NzFormModule } from 'ng-zorro-antd/form'
import { NzGridModule } from 'ng-zorro-antd/grid'
import { NzIconModule } from 'ng-zorro-antd/icon'
import { NzInputModule } from 'ng-zorro-antd/input'
import { NzInputNumberModule } from 'ng-zorro-antd/input-number'
import { NzSelectModule } from 'ng-zorro-antd/select'
import { PlantByIdDto } from './plants.service'

@Component({
	selector: 'app-plant-drawer',
	standalone: true,
	imports: [
		CommonModule,
		NzButtonModule,
		NzIconModule,
		NzBadgeModule,
		NzInputModule,
		NzSelectModule,
		NzDrawerModule,
		NzGridModule,
		NzInputNumberModule,
		NzFormModule,
		FormsModule,
		ReactiveFormsModule
	],
	templateUrl: './plant-drawer.component.html'
})
export class PlantDrawerComponent implements OnInit {
	private readonly formBuilder = inject(FormBuilder)

	drawerVisible = input.required<boolean>()
	plant = input<PlantByIdDto>()

	drawerTitle = computed(() => `Updating plant: '${this.plant()?.name}'`)

	@Output() drawerClosed = new EventEmitter<boolean>()

	editRowForm: FormGroup<{
		id: FormControl<string>
		name: FormControl<string>
		plantId: FormControl<string>
	}>

	constructor() {
		effect(() => {
			const localPlant = this.plant()
			if (localPlant) {
				this.editRowForm.patchValue({
					id: localPlant.id,
					name: localPlant.name,
					plantId: localPlant.plantId
				})
			}
		})
	}

	ngOnInit(): void {
		this.editRowForm = this.formBuilder.group({
			id: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
			name: new FormControl('', {
				nonNullable: true,
				validators: [Validators.required]
			}),
			plantId: new FormControl('', {
				nonNullable: true,
				validators: [Validators.required]
			})
		})
	}

	onCancelDrawer() {
		this.editRowForm.reset()
		this.drawerClosed.emit(false)
	}

	onSubmitDrawer() {
		if (this.editRowForm.invalid) {
			for (const control of Object.values(this.editRowForm.controls)) {
				if (control.invalid) {
					control.markAsDirty()
					control.updateValueAndValidity({ onlySelf: true })
				}
			}

			return
		}

		const { id, name, plantId } = this.editRowForm.value
	}
}
