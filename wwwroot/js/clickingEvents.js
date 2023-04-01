let myBtn = document.querySelectorAll('div > div:first-child > table > tbody > tr > td > button');
for (let pos = 0; pos < myBtn.length; pos++) {
    myBtn[pos].positionXY = pos;
    myBtn[pos].addEventListener("click", clickOnMyBtn);
}
myBtn[0].addEventListener('dbclick', myField);


let enemyBtn = document.querySelectorAll('div > div:last-child > table > tbody > tr > td > button');
for (let pos = 0; pos < myBtn.length; pos++) {
    enemyBtn[pos].positionXY = pos;
    //enemyBtn[pos].addEventListener("click", checkEnemy);    //для перевірок
    enemyBtn[pos].addEventListener("click", waitingToEnableBtns);
}



let btn_renew = document.querySelector('.button-renew');
btn_renew.addEventListener('click', async () => {
    for (let btn of myBtn) {
        btn.style.backgroundColor = '';
        //btn:hover.style.backgroundColor = 'yellow';
    }
    for (let btn of enemyBtn) {
        btn.style.backgroundColor = '';            
    }

    let response = await fetch("/Game/renew");
    let res = response.json;

    let h3 = document.querySelector('h3');
    if (h3 != null) {
        h3.remove();
    }

    for (let i = 0; i < enemyBtn.length; i++) {
        enemyBtn[i].disabled = false;
    }

    if (res == false) {
        console.log("Renew error!");
    }
});





async function clickOnMyBtn() {

    let pos = this.positionXY;

    if (this.style.backgroundColor == 'darkslateblue') {
        alert("Ця клітинка вже вибрана!");
        return;
    }

    let obj = {
        /*xy: {
            x: Math.trunc((pos / 10)),
            y: pos - (Math.trunc((pos/10)) * 10)
        },
        answer: null*/
        x: Math.trunc((pos / 10)),
        y: pos - (Math.trunc((pos / 10)) * 10),
        answer: -1
    };



    let response = await fetch("/Game/click", {
        method: "POST",
        headers: {
            'Accept': 'application/json; charset=utf-8',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify(obj
            //{
                /*x: Math.trunc((pos / 10)),
                y: pos - (Math.trunc((pos / 10)) * 10),
                answer: -1*/
              

                // на потім
                // 0 - alive, 1 - there is ship(cell), 2 - dead, 3 - shooted,


                /*my_battle_field: 2,
                enemy_battle_field: 4*/
            //}
        )
    });

    // 0 - notNew, 1 - new
    let message = await response.json();

    if (message.answer == 1) {
        this.style.backgroundColor = 'darkslateblue';
    }
    else if (message.answer == 0) {
        alert("Неможна вибрати дану клітинку!");
    }
    else {
        alert("Bи викоритсали всі кораблі!");
    }
}

/*body: JSON.stringify({
    xy: this.positionXY,
    answer: null
})*/




async function checkEnemy() {
    
    let pos = this.positionXY;

    let obj = {
        /*x: Math.trunc((pos / 10)),
        y: pos - (Math.trunc((pos / 10)) * 10),
        answer: -1,*/
        arr: [2,3
            /*[1, 2],
            [5, 6]*/
        ]
    };

    let arr = [[3, 8],[8, 8]];

    let response = await fetch("/Game/checkEnemy", {
        method: "POST",
        headers: {
            'Accept': 'application/json; charset=utf-8',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify(arr)
    });

    let message = await response.json();

    for (let someArr of message) {
        //let x = Math.trunc((pos / 10));
        //let y = pos - (Math.trunc((pos / 10)) * 10);
        for (let pos of someArr) {
            enemyBtn[pos].style.backgroundColor = 'darkslateblue';
        }
    }   
}


// власне поле
async function myField() {
    //4
    myBtn[0].style.backgroundColor = 'darkslateblue';
    myBtn[1].style.backgroundColor = 'darkslateblue';
    myBtn[2].style.backgroundColor = 'darkslateblue';
    myBtn[3].style.backgroundColor = 'darkslateblue';
    //3
    myBtn[20].style.backgroundColor = 'darkslateblue';
    myBtn[21].style.backgroundColor = 'darkslateblue';
    myBtn[22].style.backgroundColor = 'darkslateblue';
    //3
    myBtn[40].style.backgroundColor = 'darkslateblue';
    myBtn[41].style.backgroundColor = 'darkslateblue';
    myBtn[42].style.backgroundColor = 'darkslateblue';
    //2
    myBtn[60].style.backgroundColor = 'darkslateblue';
    myBtn[61].style.backgroundColor = 'darkslateblue';
    //2
    myBtn[80].style.backgroundColor = 'darkslateblue';
    myBtn[81].style.backgroundColor = 'darkslateblue';
    //2
    myBtn[99].style.backgroundColor = 'darkslateblue';
    myBtn[98].style.backgroundColor = 'darkslateblue';
    //1
    myBtn[9].style.backgroundColor = 'darkslateblue';
    //1
    myBtn[29].style.backgroundColor = 'darkslateblue';
    //1
    myBtn[49].style.backgroundColor = 'darkslateblue';
    //1
    myBtn[69].style.backgroundColor = 'darkslateblue';

    let response = await fetch("/Game/myField");
    //let message = await response.json;
}









async function clickToAttack() {

    if (this.style.backgroundColor == 'red' || this.style.backgroundColor == 'black') {
        alert("Ця клітинка вже вибрана! Точніше знищена!");
        backToenable();
        return;
    }

    // перевірка на заповнеінсть

    let responseOfFull = await fetch("/Game/checkIsFull");
    let answer = await responseOfFull.json();
    if (answer == false) {
        alert("Виберіть всі кораьлі!");
        backToenable();
        return;
    }
    //

    let pos = this.positionXY;

    let obj = {
        x: Math.trunc((pos / 10)),
        y: pos - (Math.trunc((pos / 10)) * 10),
        answer: [-1, -2]
    };

    let response = await fetch("/Game/attacking", {
        method: "POST",
        headers: {
            'Accept': 'application/json; charset=utf-8',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify(obj)
    });

    // 0 - не влучив, 1 - влучив, -1 - знищив; для ворога аналогічно  
    let message = await response.json();

    let firstInterval = 500;
    let secondInterval = 2000;

    switch (message) {
        case 1:
            {               // влучив
                this.style.backgroundColor = 'red';

                let timer = setInterval(() => {
                    if (this.style.backgroundColor == 'black') {
                        this.style.backgroundColor = 'red';
                    } else if (this.style.backgroundColor == 'red') {
                        this.style.backgroundColor = 'black';
                    }
                }, firstInterval)

                setTimeout(() => {
                    this.style.backgroundColor = 'red';
                    clearInterval(timer);

                    setTimeout(() => {
                        backToenable();
                    }, firstInterval);

                }, secondInterval);                     //невдача таким способом   // спрацювало                            //   через css

                break;
            }
        case 0:
            {               // не влучив
                this.style.backgroundColor = 'black';

                let timer = setInterval(() => {
                    if (this.style.backgroundColor == 'black') {
                        this.style.backgroundColor = 'white';
                    } else if (this.style.backgroundColor == 'white') {
                        this.style.backgroundColor = 'black';
                    }
                }, firstInterval)

                setTimeout(async () => {
                    this.style.backgroundColor = 'black';
                    clearInterval(timer);


                    // атака ворога

                    do {

                        let responseEnemy = await fetch("/Game/attackAnswer");

                        let answer = await responseEnemy.json();

                        if (answer == -1) {
                            console.log("Результат атаки був пустий!");
                            break;
                        }

                        if (answer == -2) {
                            break;
                        }

                        let arrOfTimeout = [];

                        for (let underArr of answer) {

                            if (underArr[0] != 11) {

                                if (myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor != 'red') {

                                    if (getComputedStyle(myBtn[underArr[0] * 10 + underArr[1]]).backgroundColor == 'rgb(173, 216, 230)') {    //lightblue
                                        myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor = 'black';
                                    }
                                    if (getComputedStyle(myBtn[underArr[0] * 10 + underArr[1]]).backgroundColor == "rgb(72, 61, 139)") {       //darkslateblue
                                        myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor = 'red';
                                    }

                                    let color = myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor;

                                    let timer2;
                                    if (color == 'red') {
                                        timer2 = setInterval(() => {
                                            if (myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor == 'black') {
                                                myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor = 'red';
                                            } else if (myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor == 'red') {
                                                myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor = 'black';
                                            }
                                        }, firstInterval);
                                    }

                                    if (color == 'black') {
                                        timer2 = setInterval(() => {
                                            if (myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor == 'black') {
                                                myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor = 'white';
                                            } else if (myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor == 'white') {
                                                myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor = 'black';
                                            }
                                        }, firstInterval);
                                    }

                                    let interval = setTimeout(() => {
                                        clearInterval(timer2);

                                        myBtn[underArr[0] * 10 + underArr[1]].style.backgroundColor = color;
                                    }, secondInterval);

                                    arrOfTimeout.push(interval);
                                }
                                else {
                                    console.log('Повернулася вже атакована клітина!');
                                }
                            }
                            else
                            {
                                setTimeout(() => {
                                    for (let someTimeout of arrOfTimeout) {
                                        clearTimeout(someTimeout);
                                    }

                                    deadShip(myBtn[((answer[0][0]) * 10 + (answer[0][1]))], 1, answer);
                                }, secondInterval);
                               
                                arrOfTimeout = [];
                            }
                        }

                        if (answer[answer.length - 1][0] != 11) {
                            break;
                        }

                    } while (true);

                    //

                    // повертаємо нажаття на кнопки
                    setTimeout(() => {
                        backToenable();
                    }, secondInterval);
                    //

                    // перевірка чи не кінець, користувач програв
                    let isEnd = await fetch("/Game/isEnd/0"); // 0 - мій, 1 - ворога
                    let isEndRes = await isEnd.json();
                    if (isEndRes == true) {

                        let divWithH = document.querySelector("main > div > div");

                        let h3ToInsert = document.createElement('h3');
                        h3ToInsert.textContent = "Ви програли!";

                        divWithH.append(h3ToInsert);

                        alert("Ви програли!");

                        // унеможливлюємо нажаття на кнопки
                        for (let i = 0; i < enemyBtn.length; i++) {
                            enemyBtn[i].disabled = true;
                        }


                        let enemyFieldLication = await fetch("/Game/enemyField");
                        let resOfEnemyLocation = await enemyFieldLication.json();

                        for (let i = 0; i < resOfEnemyLocation.length; i++) {
                            for (let j = 0; j < resOfEnemyLocation[i].length; j++) {
                                if (enemyBtn[resOfEnemyLocation[i][j]].style.backgroundColor != 'red') {
                                    enemyBtn[resOfEnemyLocation[i][j]].style.backgroundColor = 'green';
                                }
                            }
                        }
                    }
                    //

                }, secondInterval);

                break;
            }
        case -1:
            {
                this.style.backgroundColor = 'red';
                setTimeout(async () => {
                    let deadShipAnswer = await fetch(`/Game/deadEnemyShip/${pos}`);
                    let deadShipArr = await deadShipAnswer.json();
                    if (deadShipArr == -1) {
                        console.log("Не знайшло знищений корабель!");
                        return;
                    }
                    deadShip(this, 0, deadShipArr);
                    setTimeout(() => {
                        backToenable();
                    }, secondInterval);
                }, firstInterval);

                // перевірка чи не кінець, виграв користувач
                let isEnd = await fetch("/Game/isEnd/1");
                let isEndRes = await isEnd.json();
                if (isEndRes == true) {
                    let divWithH = document.querySelector("main > div > div");

                    let h3ToInsert = document.createElement('h3');
                    h3ToInsert.textContent = "Ви виграли!";

                    divWithH.append(h3ToInsert);

                    alert("Ви виграли!");
                    
                    // унеможливлюємо нажаття на кнопки
                    for (let i = 0; i < enemyBtn.length; i++) {
                        enemyBtn[i].disabled = true;
                    }
                }
                //

                break;
            }
        default:
            {
                console.log("clickToAttack error!");
                setTimeout(() => {
                    backToenable();         
                }, secondInterval);

                break;
            } 
    }

    /*while (canFinish == false) {

    }*/
    return true;
}


async function waitingToEnableBtns() {

    //  робимо неможливим натискання на кнопки
    for (let i = 0; i < enemyBtn.length; i++) {
        enemyBtn[i].disabled = true;
    }
    //

    let go = clickToAttack.bind(this);

    go();

    /*
    //  робимо можливим натискання на кнопки знову!
    for (let i = 0; i < enemyBtn.length; i++) {
        enemyBtn[i].disable = false;
    }
    //
    */

    // не працює, попробувати обгорнути в проміс функцію виклику сервера
}

function backToenable() {
    //  робимо можливим натискання на кнопки знову!
    setTimeout(() => {
        for (let i = 0; i < enemyBtn.length; i++) {
            enemyBtn[i].disabled = false;
        }
    }, 500);
    //
    return true;
}


//ще все не прцює але вже близко!!!!!!!!!!!!!!!1
function deadShip(lastBtn, fieldIndex, someArr) {
    let pos = lastBtn.positionXY;

    let arrValue = [
        [-11, -10, -9],
        [-1, 0, 1],
        [9, 10, 11]
    ];

    let nextPos = [-1, -1, -1, -1, -1];
    nextPos[0] = pos;


    let someBtn;
    if (fieldIndex == 0) {
        someBtn = enemyBtn;

        for (let position of someArr) {
            if (position[0] != 11) {
                let somePos = position[0] * 10 + position[1];

                try {
                    for (let i = 0; i < 3; i++) {
                        for (let j = 0; j < 3; j++) {

                            if ((somePos % 10) == 0) {
                                if (j == 0) {
                                    continue;
                                }
                            }

                            if ((somePos % 10) == 9) {
                                if (j == 2) {
                                    continue;
                                }
                            }

                            if (somePos < 10) {
                                if (i == 0) {
                                    continue;
                                }
                            }

                            if (somePos > 90) {
                                if (i == 2) {
                                    continue;
                                }
                            }

                            if ((somePos + arrValue[i][j] < 0) || (somePos + arrValue[i][j] > 99)) {
                                continue;
                            }

                            if (someBtn[somePos + arrValue[i][j]].style.backgroundColor != 'red') {
                                someBtn[somePos + arrValue[i][j]].style.backgroundColor = 'black';
                            }
                        }
                    }
                }
                catch (err) {
                    console.log(err);
                    console.log(err.stack);
                    console.log(someBtn);
                }
            }
        }

        // не вийшло так практично як хотілося
        /*for (let k = 1; k < 5; k++) {
            for (let i = 0; i < 3; i++) {
                for (let j = 0; j < 3; j++) {
                    if (arrValue[i][j] != 0) {

                        if ((pos % 10) == 0) {
                            if (j == 0) {
                                continue;
                            }
                        }

                        if ((pos % 10) == 9) {
                            if (j == 2) {
                                continue;
                            }
                        }

                        if (pos < 10) {
                            if (i == 0) {
                                continue;
                            }
                        }

                        if (pos > 90) {
                            if (i == 2) {
                                continue;
                            }
                        }

                        if ((pos + arrValue[i][j] < 0) || (pos + arrValue[i][j] > 99)) {
                            continue;
                        }

                        try {
                            if (someBtn[pos + arrValue[i][j]].style.backgroundColor != 'red') {
                                someBtn[pos + arrValue[i][j]].style.backgroundColor = 'black';
                            } else {
                                if (k == 1) {
                                    nextPos[1] = pos + arrValue[i][j];
                                    nextPos[2] = pos + arrValue[i][j];
                                    nextPos[3] = pos + arrValue[i][j];
                                    nextPos[4] = pos + arrValue[i][j];
                                } else {
                                    if (nextPos[0] < nextPos[1]) {
                                        if (nextPos[k - 1] < (pos + arrValue[i][j])) {
                                            nextPos[k] = pos + arrValue[i][j];
                                        }
                                    } else {
                                        if (nextPos[k - 1] > (pos + arrValue[i][j])) {
                                            nextPos[k] = pos + arrValue[i][j];
                                        }
                                    }
                                }
                            }     
                        }
                        catch (err) {
                            console.log(err);
                            console.log(err.stack);
                            console.log(someBtn);
                        }
                    }
                }
            }
            if (nextPos[k] != -1) {
                pos = nextPos[k];
            } else {
                return;
            }
        }*/
    } else if (fieldIndex == 1) {
        someBtn = myBtn;

        for (let position of someArr) {
            if (position[0] != 11) {
                let somePos = position[0] * 10 + position[1];

                try {
                    for (let i = 0; i < 3; i++) {
                        for (let j = 0; j < 3; j++) {

                            if ((somePos % 10) == 0) {
                                if (j == 0) {
                                    continue;
                                }
                            }

                            if ((somePos % 10) == 9) {
                                if (j == 2) {
                                    continue;
                                }
                            }

                            if (somePos < 10) {
                                if (i == 0) {
                                    continue;
                                }
                            }

                            if (somePos > 90) {
                                if (i == 2) {
                                    continue;
                                }
                            }

                            if ((somePos + arrValue[i][j] < 0) || (somePos + arrValue[i][j] > 99)) {
                                continue;
                            }

                            if (someBtn[somePos + arrValue[i][j]].style.backgroundColor != 'red') {
                                someBtn[somePos + arrValue[i][j]].style.backgroundColor = 'black';
                            }
                        }
                    }
                }
                catch (err) {
                    console.log(err);
                    console.log(err.stack);
                    console.log(someBtn);
                }
            }
        }
    }
}