import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Carer } from 'src/app/_models/carer';

@Component({
  selector: 'app-carer-card',
  templateUrl: './carer-card.component.html',
  styleUrls: ['./carer-card.component.css']
})
export class CarerCardComponent implements OnInit {
  @Input() carer: Carer;

  @Output()
  showContractScreen = new EventEmitter<any>();

  carerToSend: any = {};

  constructor() { }

  ngOnInit() {
  }

  contract(name: string, fareForHour: number, id: number){
    this.carerToSend.id = id;
    this.carerToSend.fareForHour = fareForHour;
    this.carerToSend.name = name;

    this.showContractScreen.emit(this.carerToSend);
  }

}
