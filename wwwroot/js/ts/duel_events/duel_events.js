var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (g && (g = 0, op[0] && (_ = 0)), _) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var myBtns = document.querySelectorAll("table > tbody > tr > td > button");
for (var i = 0; i < myBtns.length / 2; i++) {
    myBtns[i].positionXY = i;
    myBtns[i].addEventListener("click", clickOnMyBtn);
}
for (var i = myBtns.length / 2; i < myBtns.length; i++) {
    myBtns[i].positionXY = i - (myBtns.length / 2);
    myBtns[i].disabled = true;
    myBtns[i].addEventListener("click", clickAtack);
}
var enabledDone = false;
myBtns[0].removeEventListener('click', clickOnMyBtn);
myBtns[0].addEventListener("click", function () { return __awaiter(void 0, void 0, void 0, function () {
    var _fetch, res, arrShips;
    return __generator(this, function (_a) {
        switch (_a.label) {
            case 0: return [4 /*yield*/, fetch("/Game_duel/MyFieldQuick", {
                    method: "GET",
                    headers: {
                        'Accept': 'application/json; charset=utf-8',
                        'Content-Type': 'application/json;charset=UTF-8'
                    }
                })];
            case 1:
                _fetch = _a.sent();
                return [4 /*yield*/, _fetch.json()];
            case 2:
                res = _a.sent();
                arrShips = res.split(/[\ '\n']/).filter(function (word) {
                    if (word != "") {
                        return word;
                    }
                });
                arrShips.forEach(function (item) {
                    var num = parseInt(item);
                    myBtns[num].style.background = 'darkslateblue';
                });
                if (!enabledDone) {
                    EnableAll();
                }
                return [2 /*return*/];
        }
    });
}); });
function clickOnMyBtn() {
    return __awaiter(this, void 0, void 0, function () {
        var pos, XY_obj, urlParams, sender, responce, answer;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    pos = this.positionXY;
                    if (this.style.backgroundColor == 'darkslateblue') {
                        alert("Ця клітинка вже вибрана!");
                        return [2 /*return*/];
                    }
                    XY_obj = {
                        x: Math.trunc((pos / 10)),
                        y: pos - (Math.trunc((pos / 10)) * 10),
                        answer: null
                    };
                    urlParams = new URLSearchParams(window.location.search);
                    sender = urlParams.get('sender');
                    return [4 /*yield*/, fetch("/Game_duel/clickMyBtn?"
                            + encodeURIComponent("sender") + "=" + encodeURIComponent(sender), {
                            method: "POST",
                            headers: {
                                'Accept': 'application/json; charset=utf-8',
                                'Content-Type': 'application/json;charset=UTF-8'
                            },
                            body: JSON.stringify(XY_obj)
                        })];
                case 1:
                    responce = _a.sent();
                    return [4 /*yield*/, responce.json()];
                case 2:
                    answer = _a.sent();
                    if (answer == 1) {
                        this.style.backgroundColor = 'darkslateblue';
                    }
                    else if (answer == 0) {
                        alert("Неможна вибрати дану клітинку!");
                    }
                    else {
                        alert("Bи викоритсали всі кораблі!");
                    }
                    if (!enabledDone) {
                        EnableAll();
                    }
                    return [2 /*return*/];
            }
        });
    });
}
//import * as signalR from "../../../lib/microsoft/signalr/dist/browser/signalr.js";
// в .js файлі коментуємо дану стрічку і вона буде брати дане значення з бібліотеки, яка
// записана в .html файлі
// дикий костиль
var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();
hubConnection.serverTimeoutInMilliseconds = 600000;
hubConnection.start()
    .then(function () {
    console.log("Connect is ok!(duel_events)");
})
    .catch(function (err) {
    return console.error(err.toString());
});
var getEnemyField = false;
function clickAtack() {
    return __awaiter(this, void 0, void 0, function () {
        var pos, obj, urlParams, sender, enemyEmail, response, message;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    if (this.style.backgroundColor == 'red' || this.style.backgroundColor == 'black') {
                        alert("Ця клітинка вже вибрана! Точніше знищена!");
                        return [2 /*return*/];
                    }
                    //перевірка чи заповнив все супротивник
                    if (!getEnemyField) {
                        alert("Очікуємо противника!");
                        return [2 /*return*/];
                    }
                    pos = this.positionXY;
                    obj = {
                        x: Math.trunc((pos / 10)),
                        y: pos - (Math.trunc((pos / 10)) * 10),
                        answer: []
                    };
                    urlParams = new URLSearchParams(window.location.search);
                    sender = urlParams.get('sender');
                    enemyEmail = urlParams.get('enemyEmail');
                    return [4 /*yield*/, fetch("/Game_duel/Attacking?" +
                            encodeURIComponent("enemyEmail") + "=" + encodeURIComponent(enemyEmail) + "&" +
                            encodeURIComponent("sender") + '=' + encodeURIComponent(sender), {
                            method: "POST",
                            headers: {
                                'Accept': 'application/json; charset=utf-8',
                                'Content-Type': 'application/json;charset=UTF-8'
                            },
                            body: JSON.stringify(obj)
                        })];
                case 1:
                    response = _a.sent();
                    return [4 /*yield*/, response.json()];
                case 2:
                    message = _a.sent();
                    if (message === false) {
                        alert("Хід противника!");
                        return [2 /*return*/];
                    }
                    (AfterAttackUI.bind(this))(message, 0);
                    return [2 /*return*/];
            }
        });
    });
}
function EnableAll() {
    return __awaiter(this, void 0, void 0, function () {
        var counter, i, i, urlParams, EnemyEmail;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    counter = 0;
                    for (i = 0; i < myBtns.length / 2; i++) {
                        if (myBtns[i].style.backgroundColor == 'darkslateblue') {
                            counter++;
                        }
                    }
                    if (!(counter == 20)) return [3 /*break*/, 2];
                    for (i = myBtns.length / 2; i < myBtns.length; i++) {
                        myBtns[i].disabled = false;
                    }
                    enabledDone = true;
                    urlParams = new URLSearchParams(window.location.search);
                    EnemyEmail = urlParams.get('enemyEmail');
                    return [4 /*yield*/, fetch("/Game_duel/FullField", {
                            method: "PUT",
                            headers: { "Accept": "application/json", "Content-Type": "application/json" },
                            body: JSON.stringify(EnemyEmail)
                        })];
                case 1:
                    _a.sent();
                    _a.label = 2;
                case 2: return [2 /*return*/];
            }
        });
    });
}
hubConnection.on("FullField", function (EnemyField) { return __awaiter(void 0, void 0, void 0, function () {
    var res, answer;
    return __generator(this, function (_a) {
        switch (_a.label) {
            case 0: return [4 /*yield*/, fetch("/Game_duel/EnemyField", {
                    method: "PUT",
                    headers: {
                        'Accept': 'application/json; charset=utf-8',
                        'Content-Type': 'application/json;charset=UTF-8'
                    },
                    body: JSON.stringify(EnemyField)
                })];
            case 1:
                res = _a.sent();
                return [4 /*yield*/, res.json()];
            case 2:
                answer = _a.sent();
                if (answer == true) {
                    getEnemyField = true;
                }
                else {
                    alert("Error");
                }
                return [2 /*return*/];
        }
    });
}); });
function backToenable() {
    //  робимо можливим натискання на кнопки знову!
    setTimeout(function () {
        for (var i = myBtns.length / 2; i < myBtns.length; i++) {
            myBtns[i].disabled = false;
        }
    }, 500);
    //
    return true;
}
function disableAll() {
    for (var i = myBtns.length / 2; i < myBtns.length; i++) {
        myBtns[i].disabled = true;
    }
}
function AfterAttackUI(message, fieldIndex) {
    return __awaiter(this, void 0, void 0, function () {
        var firstInterval, secondInterval, _a, timer_1, timer_2, timer, isEnd, isEndRes, divWithH, h3ToInsert, i, divWithH, h3ToInsert, i, enemyFieldLication, resOfEnemyLocation, i, j;
        var _this = this;
        return __generator(this, function (_b) {
            switch (_b.label) {
                case 0:
                    firstInterval = 500;
                    secondInterval = 2000;
                    _a = message;
                    switch (_a) {
                        case 0: return [3 /*break*/, 1];
                        case 1: return [3 /*break*/, 2];
                        case -1: return [3 /*break*/, 3];
                    }
                    return [3 /*break*/, 10];
                case 1:
                    { // не влучив
                        disableAll();
                        this.style.backgroundColor = 'black';
                        timer_1 = setInterval(function () {
                            if (_this.style.backgroundColor == 'black') {
                                _this.style.backgroundColor = 'white';
                            }
                            else if (_this.style.backgroundColor == 'white') {
                                _this.style.backgroundColor = 'black';
                            }
                        }, firstInterval);
                        setTimeout(function () {
                            _this.style.backgroundColor = 'black';
                            clearInterval(timer_1);
                            setTimeout(function () {
                                backToenable();
                            }, firstInterval);
                        }, secondInterval);
                    }
                    return [3 /*break*/, 11];
                case 2:
                    { // влучив
                        disableAll();
                        this.style.backgroundColor = 'red';
                        timer_2 = setInterval(function () {
                            if (_this.style.backgroundColor == 'black') {
                                _this.style.backgroundColor = 'red';
                            }
                            else if (_this.style.backgroundColor == 'red') {
                                _this.style.backgroundColor = 'black';
                            }
                        }, firstInterval);
                        setTimeout(function () {
                            _this.style.backgroundColor = 'red';
                            clearInterval(timer_2);
                            setTimeout(function () {
                                backToenable();
                            }, firstInterval);
                        }, secondInterval);
                    }
                    return [3 /*break*/, 11];
                case 3:
                    this.style.backgroundColor = 'red';
                    timer = setTimeout(function () { return __awaiter(_this, void 0, void 0, function () {
                        var deadShipAnswer, deadShipArr;
                        return __generator(this, function (_a) {
                            switch (_a.label) {
                                case 0: return [4 /*yield*/, fetch("/Game_duel/deadEnemyShip/".concat(this.positionXY))];
                                case 1:
                                    deadShipAnswer = _a.sent();
                                    return [4 /*yield*/, deadShipAnswer.json()];
                                case 2:
                                    deadShipArr = _a.sent();
                                    if (deadShipArr == -1) {
                                        console.log("Не знайшло знищений корабель!");
                                        return [2 /*return*/];
                                    }
                                    deadShip(this, (fieldIndex == 0 ? 1 : 0), deadShipArr); // 0 - я, 1 - ворог
                                    setTimeout(function () {
                                        backToenable();
                                    }, secondInterval);
                                    return [2 /*return*/];
                            }
                        });
                    }); }, firstInterval);
                    return [4 /*yield*/, fetch("/Game_duel/isEnd/".concat(fieldIndex))];
                case 4:
                    isEnd = _b.sent();
                    return [4 /*yield*/, isEnd.json()];
                case 5:
                    isEndRes = _b.sent();
                    if (!(isEndRes == true)) return [3 /*break*/, 9];
                    if (!(fieldIndex == 0)) return [3 /*break*/, 6];
                    divWithH = document.querySelector("main > div > div > div");
                    h3ToInsert = document.createElement('h3');
                    h3ToInsert.textContent = "Ви виграли!";
                    divWithH.append(h3ToInsert);
                    alert("Ви виграли!");
                    // унеможливлюємо нажаття на кнопки                    
                    for (i = myBtns.length / 2; i < myBtns.length; i++) {
                        myBtns[i].disabled = true;
                    }
                    return [3 /*break*/, 9];
                case 6:
                    if (!(fieldIndex == 1)) return [3 /*break*/, 9];
                    divWithH = document.querySelector("main > div > div > div");
                    h3ToInsert = document.createElement('h3');
                    h3ToInsert.textContent = "Ви програли!";
                    divWithH.append(h3ToInsert);
                    alert("Ви програли!");
                    // унеможливлюємо нажаття на кнопки
                    for (i = myBtns.length / 2; i < myBtns.length; i++) {
                        myBtns[i].disabled = true;
                    }
                    return [4 /*yield*/, fetch("/Game/enemyField")];
                case 7:
                    enemyFieldLication = _b.sent();
                    return [4 /*yield*/, enemyFieldLication.json()];
                case 8:
                    resOfEnemyLocation = _b.sent();
                    for (i = 0; i < resOfEnemyLocation.length; i++) {
                        for (j = 0; j < resOfEnemyLocation[i].length; j++) {
                            if (myBtns[resOfEnemyLocation[i][j] + 100].style.backgroundColor != 'red') {
                                myBtns[resOfEnemyLocation[i][j] + 100].style.backgroundColor = 'green';
                            }
                        }
                    }
                    _b.label = 9;
                case 9: return [3 /*break*/, 11];
                case 10:
                    console.error("Server failed! Return number after checking hiting is corrupt!");
                    return [3 /*break*/, 11];
                case 11: return [2 /*return*/];
            }
        });
    });
}
hubConnection.on("Attack", function (cell, isHit) { return __awaiter(void 0, void 0, void 0, function () {
    var pos, _res, res, _res, res;
    return __generator(this, function (_a) {
        switch (_a.label) {
            case 0:
                pos = cell._x * 10 + cell._y;
                if (!(isHit == 0)) return [3 /*break*/, 3];
                return [4 /*yield*/, fetch("/Game_duel/SynchrStep", {
                        method: "PUT",
                        headers: {
                            'Accept': 'application/json; charset=utf-8',
                            'Content-Type': 'application/json;charset=UTF-8'
                        }
                    })];
            case 1:
                _res = _a.sent();
                return [4 /*yield*/, _res.json()];
            case 2:
                res = _a.sent();
                if (res == true) {
                    console.log("Synchronize step");
                }
                else {
                    alert("Error!");
                }
                return [3 /*break*/, 6];
            case 3: return [4 /*yield*/, fetch("/Game_duel/SynchrHit", {
                    method: "PUT",
                    headers: {
                        'Accept': 'application/json; charset=utf-8',
                        'Content-Type': 'application/json;charset=UTF-8'
                    },
                    body: JSON.stringify(cell)
                })];
            case 4:
                _res = _a.sent();
                return [4 /*yield*/, _res.json()];
            case 5:
                res = _a.sent();
                if (res == true) {
                    console.log("Synchronize hit");
                }
                else {
                    alert("Error synchronization!");
                }
                _a.label = 6;
            case 6:
                (AfterAttackUI.bind(myBtns[pos]))(isHit, 1);
                return [2 /*return*/];
        }
    });
}); });
function deadShip(lastBtn, fieldIndex, someArr) {
    var pos = lastBtn.positionXY;
    var arrValue = [
        [-11, -10, -9],
        [-1, 0, 1],
        [9, 10, 11]
    ];
    var nextPos = [-1, -1, -1, -1, -1];
    nextPos[0] = pos;
    var someBtn = [];
    if (fieldIndex == 0) {
        for (var i = 0; i < myBtns.length / 2; i++) {
            someBtn.push(myBtns[i]);
        }
        for (var _i = 0, someArr_1 = someArr; _i < someArr_1.length; _i++) {
            var position = someArr_1[_i];
            if (position[0] != 11) {
                var somePos = position[0] * 10 + position[1];
                try {
                    for (var i = 0; i < 3; i++) {
                        for (var j = 0; j < 3; j++) {
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
    else if (fieldIndex == 1) {
        for (var i = myBtns.length / 2; i < myBtns.length; i++) {
            someBtn.push(myBtns[i]);
        }
        for (var _a = 0, someArr_2 = someArr; _a < someArr_2.length; _a++) {
            var position = someArr_2[_a];
            if (position[0] != 11) {
                var somePos = position[0] * 10 + position[1];
                try {
                    for (var i = 0; i < 3; i++) {
                        for (var j = 0; j < 3; j++) {
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
//# sourceMappingURL=duel_events.js.map