package es.msanchez.templates.spring.starter;

import es.msanchez.templates.spring.config.SpringConfig;
import es.msanchez.templates.spring.utilities.SpringRegister;
import lombok.extern.slf4j.Slf4j;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

@Slf4j public class Main {

  public static void main(String[] args) {
    log.info("Application started");
    final AnnotationConfigApplicationContext context = prepareSpring();
    log.info("Configuration is ready");

    final DummyBean bean = context.getBean(DummyBean.class);

    log.info("Application is done");
  }

  private static AnnotationConfigApplicationContext prepareSpring() {
    final SpringRegister register = new SpringRegister();

    return register.initSpringApplicationContext(SpringConfig.class);
  }

}
