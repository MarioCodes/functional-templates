Configurated templates to start a project directly out of the box.  
All of them contain all the common Frameworks that I use to develop. The difference resides in the big Frameworks which modify the project's structure.  

#### Template Complete
##### Frameworks 
* Maven
* Spring
* Vertx

##### How-to start
```  
mvn clean install  
java -jar [name]-fat.jar -cluster
```  

#### Template Springboot
##### How-to start
```  
mvn clean install
java -jar [name].jar  
```

#### Common Frameworks
* Slf4j with Logback  
* Lombok  
* JUnit
* Mockito
* AssertJ
* BDDCatchException
