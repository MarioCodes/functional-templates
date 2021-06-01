package es.msanchez.frameworks.java.spring.boot;

import lombok.extern.slf4j.Slf4j;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.Bean;

import java.util.Arrays;
import java.util.List;

@Slf4j
@SpringBootApplication
public class Application {

    public static void main(final String[] args) {
        SpringApplication.run(Application.class, args);
    }

    @Bean
    public CommandLineRunner commandLineRunner(final ApplicationContext context) {
        return args -> {
            log.info("Beans provided by Spring Boot:");
            final List<String> beanNames = Arrays.asList(context.getBeanDefinitionNames());
            beanNames.forEach(bean -> log.debug("Bean name '{}'", bean));
        };

    }

}
