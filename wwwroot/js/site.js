// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.






//let inputs = document.querySelectorAll('div > form > div > div > input');   //document.forms.(name форми)   //https://uk.javascript.info/form-elements
//let spans = document.querySelectorAll('div > form > div > div > span');



/*for(let i = 0; i < inputs.length; i++)
{
    inputs[i].style.backgroundColor = `red`;
    inputs[i].style.color = `white`; 
}*/


//let inputs = document.forms;
/*
for (let i = 0; i < document.forms.length; i++)
{
    document.forms[i].style.backgroundColor = `red`;
    document.forms[i].style.color = `white`;
}*/


/*                                  //так і не вжалося не скидати то на сервак((((
for (let i = 0; i < 2; i++)
{
    inputs[i].onblur = function () {
        if (inputs[i].value.length == 0){                                                      
            inputs[i].classList.add('invalid');

            let spanObj = document.createElement('span');
            spanObj.className = 'text-danger';
            inputs[i].append(spanObj);
        }
    };

    inputs[i].onfocus = function () {
        if (this.classList.contains('invalid')) {
            // видалити індикатор помилки, тому що користувач хоче ввести дані заново
            this.classList.remove('invalid');

            let spanObj = document.querySelector(inputs[i].nextSibling);
            document.remove(spanObj);
        }
    };
}
*/

import { make_table } from "./making_table.js";

make_table();

/*import { clickOnMyBtn } from "./clickingEvents.js"

let myBtn = document.querySelectorAll('div > div:first-child > table > tbody > tr > td > button');
for (let pos = 0; pos < myBtn.length; pos++) {
    myBtn[pos].positionXY = pos;
    myBtn[pos].addEventListener("click", clickOnMyBtn);
}*/