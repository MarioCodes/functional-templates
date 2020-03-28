Ready-to-code templates to start a project directly out of the box.

## Compile & Run
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

### Others
I'll try to put start scripts at the root folder for the rest of them.

## Known issues
* `docker-jekyll` --reload option doesn't work
* `kotlin-spring` doesn't compile. All `kotlin-springboot` variants do though
