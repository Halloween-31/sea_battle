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
        var pos;
        return __generator(this, function (_a) {
            if (this.style.backgroundColor == 'red' || this.style.backgroundColor == 'black') {
                alert("Ця клітинка вже вибрана! Точніше знищена!");
                return [2 /*return*/];
            }
            //перевірка чи заповнив все супротивник
            if (!getEnemyField) {
                alert("Очікуємо противника!");
            }
            pos = this.positionXY;
            return [2 /*return*/];
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
                return [2 /*return*/];
        }
    });
}); });
//# sourceMappingURL=duel_events.js.map