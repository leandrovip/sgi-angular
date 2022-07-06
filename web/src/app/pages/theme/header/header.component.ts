import { Component, OnInit, Renderer2 } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  private sideStatus!: boolean;

  constructor(private renderer: Renderer2) {}

  ngOnInit(): void {
    this.sideStatus = true;
  }

  ToggleSide() {
    if (this.sideStatus) {
      this.renderer.addClass(document.body, 'toggle-sidebar');
    } else {
      this.renderer.removeClass(document.body, 'toggle-sidebar');
    }

    this.sideStatus = !this.sideStatus;
  }
}
