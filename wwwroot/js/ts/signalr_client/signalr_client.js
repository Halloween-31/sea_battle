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
//import * as signalR from "../../../lib/microsoft/signalr/dist/browser/signalr.js";
// в .js файлі коментуємо дану стрічку і вона буде брати дане значення з бібліотеки, яка
// записана в .html файлі
// дикий костиль
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();
connection.serverTimeoutInMilliseconds = 600000;
document.querySelector(".btn").addEventListener("click", function (e) { return __awaiter(void 0, void 0, void 0, function () {
    var textarea, msg, toEmail, urlParams, enemyEmail;
    return __generator(this, function (_a) {
        e.preventDefault();
        textarea = document.querySelector('.form-container > textarea');
        msg = textarea.value;
        toEmail = getCookie("EnemyEmail");
        urlParams = new URLSearchParams(window.location.search);
        enemyEmail = urlParams.get('enemyEmail');
        console.log(enemyEmail == toEmail);
        /*const user: User = await getMyName();*/
        connection.invoke("Send", msg, enemyEmail)
            .catch(function (err) {
            return console.error(err.toString());
        });
        return [2 /*return*/];
    });
}); });
var User_time = /** @class */ (function () {
    function User_time() {
    }
    return User_time;
}());
connection.on("Receive", function (message, user_time) {
    //console.log("Recieved message: " + message);
    var new_msg = document.createElement('div');
    new_msg.classList.add("div-msg");
    new_msg.innerHTML =
        "<p class=\"name-time\"><b>".concat(user_time.name + " " + user_time.surname, ": </b><span>").concat(user_time.time, "</span > </p>\n        <p class=\"msg-text\">").concat(message, "</p>");
    var div = document.querySelector('.form-container > div');
    div.append(new_msg);
    div.scrollTop = div.scrollHeight;
    var h = getComputedStyle(div).height;
    if (parseInt(h) > 250) {
        div.style.height = '250px';
    }
    var textarea = document.querySelector('.form-container > textarea');
    textarea.value = '';
});
connection.start()
    .then(function () {
    console.log("SignalR started!");
})
    .catch(function (err) {
    return console.error(err.toString());
});
var User = /** @class */ (function () {
    function User() {
    }
    return User;
}());
function getMyName() {
    return __awaiter(this, void 0, void 0, function () {
        var MyEmail, urlParams, myEmail, res, user, e_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 3, , 4]);
                    MyEmail = getCookie("MyEmail");
                    urlParams = new URLSearchParams(window.location.search);
                    myEmail = urlParams.get('myEmail');
                    console.log(MyEmail == myEmail);
                    return [4 /*yield*/, fetch("/user/GetMyName", {
                            method: "POST",
                            headers: { "Accept": "application/json", "Content-Type": "application/json" },
                            body: JSON.stringify(myEmail)
                        })];
                case 1:
                    res = _a.sent();
                    return [4 /*yield*/, res.json()];
                case 2:
                    user = _a.sent();
                    return [2 /*return*/, user];
                case 3:
                    e_1 = _a.sent();
                    console.log(e_1);
                    return [3 /*break*/, 4];
                case 4: return [2 /*return*/];
            }
        });
    });
}
function getCookie(name) {
    var matches = document.cookie.match(new RegExp("(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}
//# sourceMappingURL=signalr_client.js.map