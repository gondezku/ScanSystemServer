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
        for (let i = 0; i <= _ProdnStat.length-1; i++) {
            console.log(_ProdnStat[i].buName);
        }
    }
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