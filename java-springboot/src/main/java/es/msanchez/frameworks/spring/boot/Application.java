package es.msanchez.frameworks.spring.boot;

import java.util.Arrays;
import java.util.List;

import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.Bean;

import lombok.extern.slf4j.Slf4j;

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
      beanNames.stream().forEach(bean -> log.debug("Bean name '{}'", bean));
    };

  }

}
