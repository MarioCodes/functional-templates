package es.msanchez.templates.java.springboot.jpa.config;

import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

@Configuration
@ComponentScan(basePackages = {
        "es.msanchez.templates.java.springboot.jpa.**"
})
public class SpringConfig {
}
