1. Install RabbitMq server as docker image using:
docker run -d --hostname my-rabbit --name rabbit-name  -p 15672:15672 -p 5672:5672 rabbitmq:3-management
	1.1. Use options to RabbitMq in WebApi and Client:
	"RabbitMqConfiguration": 
	{
    "Hostname": "127.0.0.1",
    "QueueName": "CustomerQueue",
    "UserName": "guest",
    "Password": "guest"
 	}
2. Intall and setup MongoDatabase and make preparations.(docker). Using bash(docker exec -it geeksarray-mongo /bin/bash) create db and collection.
	2.1. Links to start: 
	https://geeksarray.com/blog/create-mongodb-docker-image-and-connect-from-dot-net-core-app
	https://medium.com/@kristaps.strals/docker-mongodb-net-core-a-good-time-e21f1acb4b7b (using managment studio)
3. Buid WinApi and Client to ensure communication.
4. Run docker rabbitmq - image.
	

Disadvantages:
1. WebApi service should be pucked in docker with env variables and options.
2. CoreClient should be pucked in docker with env variables and options.
3. Authorization service works synchronize.
 
