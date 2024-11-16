import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaymentDetailService {

  url:string = environment.apiBaseUrl+"/payment"
  constructor(private http: HttpClient) { }

  refreshList()
  {
    this.http.get(this.url)
    .subscribe({
      next: res=>{
        console.log(res);
      },
      error: err=>{console.log(err)}
    })
  }
}
