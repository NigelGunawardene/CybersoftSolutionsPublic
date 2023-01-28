import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-accordion',
  template: `
    <div> <ng-content select="app-accordion-item"></ng-content> </div>
  `,
  styleUrls: ['./accordion.component.scss']
})
export class AccordionComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
