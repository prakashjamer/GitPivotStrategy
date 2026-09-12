const connection = new signalR.HubConnectionBuilder()
    .withUrl('/offers')
    .build();

//this.hubConnectionBuilder = new HubConnectionBuilder().withUrl('https://localhost:7006/offers').configureLogging(LogLevel.Information).build();
//this.hubConnectionBuilder.start().then(() => console.log('Connection started.......!')).catch(err => console.log('Error while connect with server'));
//this.hubConnectionBuilder.on('SendOffersToUser', (result) => {
//    this.offers.push(result);
//});

//This method receive the message and Append to our list  
connection.on("SendOffersToUser", ( message) => {
    const msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    const encodedMsg =  " :: " + msg;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(err => console.error(err.toString()));

//Send the message  

document.getElementById("sendMessage").addEventListener("click", event => {
   
    const message = document.getElementById("userMessage").value;
    connection.invoke("SendOffersToUser", message).catch(err => console.error(err.toString()));
    event.preventDefault();
});  

