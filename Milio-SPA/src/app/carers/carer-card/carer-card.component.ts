import { Component, OnInit, Input } from '@angular/core';
import { Carer } from 'src/app/_models/carer';

@Component({
  selector: 'app-carer-card',
  templateUrl: './carer-card.component.html',
  styleUrls: ['./carer-card.component.css']
})
export class CarerCardComponent implements OnInit {
  @Input() carer: Carer;

  constructor() { }

  ngOnInit() {
  }

}
