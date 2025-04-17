//create connection
var myConn = new signalR.HubConnectionBuilder()
    //.configureLogging(signalR.LogLevel.Information)
    .withAutomaticReconnect()
    .withUrl("/hubs/User", signalR.HttpTransportType.WebSockets).build();
//connect to methods that hub invokes aka receive notfications from hub
myConn.on("updateControll", (value) => {
    console.log(value);
});

myConn.on("updateStat", (value) => {
    if (_ProdnStat.length == 0) _ProdnStat.push(value);
    if (_ProdnStat.length != 0)
    {
        //for (let i = 0; i <= _ProdnStat.length - 1; i++) {
        const result = _ProdnStat.find(item => item['buName'] === value.buName && item['lineName'] === value.lineName);
        if (!result) {
            _ProdnStat.push(value)
        } else {
            result.model = value.model;
            result.plan = value.plan;
            result.act = value.act;
        }
        //console.log(_ProdnStat);
        //}
        const test = 'Line'
        for (let i = 1; i <= _Line; i++) {
            const resultQ = _ProdnStat.find(item => item['buName'] === 'WP' && item['lineName'] === test + i);
            var elModel = $('#' + test + i + 'model')
            var elplan = $('#' + test + i + 'plan')
            var elact = $('#' + test + i + 'act')
            var eldiff = $('#' + test + i + 'diff')
            if (resultQ) {
                elModel.text(resultQ.model)
                elplan.text(resultQ.plan)
                elact.text(resultQ.act)
                var diff = resultQ.act - resultQ.plan
                eldiff.text(diff)
                if (diff >= 0) {
                    eldiff.css("color", "green")
                } else {
                    eldiff.css("color", "red")
                }
            }
        }
    }
    //const result = _ProdnStat.find(item => item['buName'] === 'WP');
    //if (!result) {
    //    _ProdnStat.push(value)
    //} else {
    //    const filter = _ProdnStat.filter(item => item['buName'] === 'WP');
    //    console.log(filter)
    //    _ProdnStat.push(value)
    //}
});

//invoke hub methods aka send notification to hub
function Get() {
    myConn.send("GetMonCtrl","Line1")
}


//start connection

function fulfilled() {
    //do something on start
    console.log("Connection to User Hub Successful");
    //Get();
    /*myConn.send("MonCtrl", "Line1", false, false,10,"Test")*/
}
function rejected() {
    //rejected logs
}


myConn.start().then(fulfilled, rejected)