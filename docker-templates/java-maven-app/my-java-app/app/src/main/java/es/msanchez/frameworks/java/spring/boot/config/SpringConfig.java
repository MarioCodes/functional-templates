package es.msanchez.frameworks.java.spring.boot.config;

import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

@Configuration
@ComponentScan(basePackages = { "es.msanchez.frameworks.java.spring.boot.**" })
public class SpringConfig {

}
