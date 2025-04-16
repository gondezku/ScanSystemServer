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
    console.log("StatsUpdates :")
    console.log(value);
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