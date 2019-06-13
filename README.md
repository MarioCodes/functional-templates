Configurated templates ready to clone and start a project directly out of the box. All of them are built through Maven.  

There're 2 big groups:  
* `java-kotlin` they support both Java and Kotlin code at the same time.
* `java` they only support Java code.

### How to compile & launch
#### Spring(boot)
They compile as a fat jar.
~~~ bash
mvn clean install
java -jar [name].jar  
~~~

#### Vertx
They compile the same but the `-cluster` option is needed for `Hazelcast` and `Vertx` to search for another microservices on launch.
~~~ bash
mvn clean install  
java -jar [name]-fat.jar -cluster
~~~

#### Spring data JPA with Database
I included a `MySQL8` version as a `Docker` service. To run it, be sure to have `Docker` installed, go to the `/database` folder and run
~~~ bash
docker-compose up -d
~~~
After this just follow the same steps as for `Spring(boot)` to compile and launch.
