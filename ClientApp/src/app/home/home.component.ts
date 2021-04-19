import { Component } from '@angular/core';

export interface Tile {
  cols: number;
  rows: number;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  leftFirstTile: Tile = {cols: 1, rows: 1};
  leftSecondTile: Tile = {cols: 1, rows: 1};
  leftThirdTile: Tile = {cols: 1, rows: 4};
  leftFourthTile: Tile = {cols: 1, rows: 2};
  rightFirstTile: Tile = {cols: 1, rows: 3};
  rightSecondTile: Tile = {cols: 1, rows: 4};
  bottomTile: Tile = {cols: 2, rows: 0.5};

}
