var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);

server.listen(3000);


var ConnectedClients =[];
var PlayersOnline =[];
var DiscconectTime = 300;
var Sockets="";
var InCommingConnetions = "FALSE";


var Update = function()
{
	

  for (var i = 0; i< ConnectedClients.length; i++) 
   {
   	if(ConnectedClients[i] != null)
   	{
   	
   		if(ConnectedClients[i].UsersState === "2" || ConnectedClients[i].UsersState === "1" )
   		{
 	     ConnectedClients[i].IdelTime++;
 	     
 	    }
 	  if(ConnectedClients[i].IdelTime > DiscconectTime)
 	  {
 	  
 	  	ConnectedClients[i].UsersState = 0;
 	  	Sockets.emit('OnPlayerDisconnect',ConnectedClients[i]);
	    Sockets.broadcast.emit('OnPlayerDisconnect',ConnectedClients[i]);
 	  	ConnectedClients.splice(ConnectedClients.indexOf(i),1);
 	  	PlayersOnline.splice(PlayersOnline.indexOf(i),1);
 	  	InCommingConnetions="FALSE";
 	  }

 	}


 	}

}
setInterval(function(){Update()},500);



io.on('connection',function(socket)
{

Sockets =socket; 
InCommingConnetions = "FALSE"



socket.on('OnConnection',function(data)
{
	//console.log("Data: "+ JSON.stringify(data));
 
 
      if(data.UsersState === "2")
        {


        	for (var i = 0; i< ConnectedClients.length; i++) 
                 {

                 	

                 	if(ConnectedClients[i].UserID === data.UserID)
 	                	{
 	                		ConnectedClients[i].UsersState = data.UsersState;
 	                		socket.emit('OnPlayerConnected', data);
 	                		socket.broadcast.emit('OnPlayerConnected', data);
 	                		if(InCommingConnetions ==="TRUE")
 	                		{
 	                		 
 	                		 InCommingConnetions = "FALSE";
 	                		 PlayersOnline.push(data);
 	                		}
 	                		
 	                		return;
 	                	}


                 }
                 if(InCommingConnetions ==="FALSE")
                 {
	             ConnectedClients.push(data);
	             InCommingConnetions="TRUE";
	             
	             }
        }
        
	
   


     
if(data.UsersState === "1")
{


                        for (var j = 0; j< PlayersOnline.length; j++) 
 	                    {
        
                         if(PlayersOnline[j].UserID === data.UserID)
                         {
                         	
                         	PlayersOnline[j].UsersState = 0;
                         	Sockets.emit('OnPlayerDisconnect',PlayersOnline[j]);
	                        Sockets.broadcast.emit('OnPlayerDisconnect',PlayersOnline[j]);
 	  	                    ConnectedClients.splice(ConnectedClients.indexOf(i),1);
 	  	                    PlayersOnline.splice(PlayersOnline.indexOf(i),1);
                         	
                         	InCommingConnetions="FALSE";
                         	return;
                         }
 	  
 	                    }
if(InCommingConnetions ==="FALSE")
{
ConnectedClients.push(data);
socket.emit('OnPlayerConnected', data);
InCommingConnetions="TRUE";
}
}


ConnectedClients.forEach(function(data)
{
 socket.broadcast.emit('OnPlayerConnected', data);
});
 console.log("Connected Clients: "+ConnectedClients.length);
});



socket.on('OnPlayerActions',function(data)
{
 for (var i = 0; i< ConnectedClients.length; i++) 
      {
 	   if(ConnectedClients[i] != null )
 	   {
 	   	if(ConnectedClients[i].UserID === data.UserID)
 	   	{
 	   	ConnectedClients[i].IdelTime = 0;
 	    }
 	   }
     }

socket.broadcast.emit('OnPlayerActions', data);
});

socket.on('OnPlayerDisconnect',function(data)
{
       for(var i =0; i<ConnectedClients.length; i++)
       {
       	if(ConnectedClients[i] != null )
 	    {
       	   if(ConnectedClients[i].UserID === data.UserID)
       	  {
       	  	ConnectedClients[i].UsersState = 0;
       	  
           socket.emit('OnPlayerDisconnect',data);
	       socket.broadcast.emit('OnPlayerDisconnect',data);
	       ConnectedClients.splice(ConnectedClients.indexOf(i),1);
	       PlayersOnline.splice(PlayersOnline.indexOf(i),1);
	       InCommingConnetions="FALSE";
       	  }
        }
       }
});

});