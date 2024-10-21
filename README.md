My own ready-to-code templates to start a project directly out of the box.

## .NET 8 (updated to C# 12)
* `basic-template`: my most simple template, ready to start. You're able to set config at appsettings.json. For really basic features. 
* `crud-memory-ddbb` it's the same as `basic-template` but ready with an *in-memory* database to be able to use Entity Framework Core
* `multi-project` the best one for serious personal projects and interview challenges. this project includes: complete unit testing & coverlet usage *(to see code and branch coverage)*.
  * the main project to open is `Api.Core`
  * this is a template. don't try to run it, as important private info is missing from the repository
* `test-features` not so much a template, but a project I use for really quick testing where I leave code examples for things I may use in the future. Right now, it includes:
  * how to use custom configuration
  * how to use custom middleware
  * how to use dotnet validation for models
  * how to documentate methods
  * EF Core in-memory database
  * testing data generation for EF Core
  * global query filters for EF Core
  * really basic testing with NUnit

## Docker
To update, as they are really old

## Kubernetes
To update, as I didn't really know what I was doing back when I created them

---
## Outdated
I don't really work with Java, Kotlin or Android anymore, but I leave this for reasons

### Java & Kotlin
They compile as a fat jar through maven.  
Kotlin is first compiled to java code, and this to binaries. This way it's possible to write Java classes at Kotlin projects for ease of use.

~~~ bash
mvn clean install
java -jar [name].jar  
~~~

### Java Vertx
They compile the same but the `-cluster` option is needed for `Hazelcast` and `Vertx` to search for another microservices on launch.
~~~ bash
mvn clean install  
java -jar [name]-fat.jar -cluster
~~~

### Docker
Everything that comes built through docker, comes with a Dockerfile and a docker-compose script so just `docker-compose up` at the script level to start the service.

#### Java
It uses a multi-stage image which builds upon a maven image and then deploys the resultant .jar into a Java image.

#### Jekyll
The build.sh script builds the image from docker-compose.yml and DockerFile, but does not start the container. This is useful to deploy into DockerHub when all changes are ready.
development.sh script builds the image from cache and starts a container with --watch option. This is useful to write posts, as it will automatically reload all changes on .md files.

#### mysql & mongodb
Official images, without ini script. Initializes a blank database.
