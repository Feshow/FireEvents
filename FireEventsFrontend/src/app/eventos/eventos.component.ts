import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent {
  public events: any = [];
  public filteredEventsList: any = [];
  widthImg: number = 130;
  marginImg: number = 1;
  showImg: boolean = true;
  private filterValue: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get('https://localhost:7104/api/eventos').subscribe(
      (response) => {
        this.events = response;
        this.filteredEventsList = this.events;
      },
      (error) => console.log(error)
    );
  }

  showImageToggle() {
    this.showImg = !this.showImg;
  }

  public get listFilter(): string {
    return this.filterValue;
  }

  public set listFilter(value: string) {
    this.filterValue = value;
    this.filteredEventsList = this.listFilter
      ? this.filteringList(this.listFilter)
      : this.events;
  }

  filteringList(listFilter: string): any {
    listFilter = listFilter.toLocaleLowerCase();
    return this.events.filter(
      (evento: { tema: string; endereco: string }) =>
        evento.tema.toLocaleLowerCase().indexOf(listFilter) !== -1 ||
        evento.endereco.toLocaleLowerCase().indexOf(listFilter) !== -1
    );
  }
}
