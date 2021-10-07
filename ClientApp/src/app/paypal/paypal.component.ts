import { Component, OnInit, ViewChild } from '@angular/core';
import { IPayPalConfig, ICreateOrderRequest } from 'ngx-paypal';
import { ElementRef } from '@angular/core';

  @Component({
    templateUrl: './paypal.component.html',
  })
  export class PaypalComponent implements OnInit {

    public payPalConfig?: IPayPalConfig;
    private payPalID = 'AeEiO1jzqkISYf1_qru9moGMmr_QxY6eCZJf3Pgh80jTHWRwxBpO7VNQWdreA9DRZqSk-N0DmX_vgiru'
    showSuccess: boolean

    ngOnInit(): void {
      this.initConfig();
    }

    private initConfig(): void {
      this.payPalConfig = {
      currency: 'CAD',
      clientId: this.payPalID,
      createOrderOnClient: (data) => <ICreateOrderRequest>{
        intent: 'CAPTURE',
        purchase_units: [
          {
            amount: {
              currency_code: 'CAD',
              value: '9.99',
              breakdown: {
                item_total: {
                  currency_code: 'CAD',
                  value: '9.99'
                }
              }
            },
            items: [
              {
                name: 'Enterprise Subscription',
                quantity: '1',
                category: 'DIGITAL_GOODS',
                unit_amount: {
                  currency_code: 'CAD',
                  value: '9.99',
                },
              }
            ]
          }
        ]
      },
      advanced: {
        commit: 'true'
      },
      style: {
        label: 'paypal',
        layout: 'vertical'
      },
      onApprove: (data, actions) => {
        console.log('onApprove - transaction was approved, but not authorized', data, actions);
        actions.order.get().then(details => {
          console.log('onApprove - you can get full order details inside onApprove: ', details);
        });
      },
      onClientAuthorization: (data) => {
        console.log('onClientAuthorization - you should probably inform your server about completed transaction at this point', data);
        this.showSuccess = true;
      },
      onCancel: (data, actions) => {
        console.log('OnCancel', data, actions);
      },
      onError: err => {
        console.log('OnError', err);
      },
      onClick: (data, actions) => {
        console.log('onClick', data, actions);
      },
    };
    }
  }