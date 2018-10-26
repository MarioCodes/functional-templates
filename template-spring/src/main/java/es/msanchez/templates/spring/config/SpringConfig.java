package es.msanchez.templates.spring.config;

import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

@Configuration @ComponentScan(basePackages = { "es.msanchez.templates.spring.**.*" }) public class SpringConfig {

}
