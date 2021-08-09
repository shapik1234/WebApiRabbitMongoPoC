1. Install RabbitMq server as docker image using:
docker run -d --hostname my-rabbit --name rabbit-name  -p 15672:15672 -p 5672:5672 rabbitmq:3-management
2. Intall and setup MongoDatabase and make preparations.
3. Buid WinApi and Client to ensure communication.