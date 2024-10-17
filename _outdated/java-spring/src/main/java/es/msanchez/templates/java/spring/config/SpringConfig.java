package es.msanchez.templates.java.spring.config;

import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

@Configuration
@ComponentScan(basePackages = {"es.msanchez.templates.java.spring.**.*"})
public class SpringConfig {

}
