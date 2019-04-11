package es.msanchez.templates.complete.config;

import io.vertx.core.Vertx;
import io.vertx.core.eventbus.EventBus;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

@Configuration @ComponentScan(basePackages = { "es.msanchez.templates.complete.**.*" })
public class SpringConfig {

  /**
   * Use this always to obtain a Vertx instance. Otherwise, if we create two instances we may have two different EventBuses and the messages
   * won't reach their destination.
   * Do it through {@link org.springframework.context.ApplicationContext#getBean(String) any of the .getBean() methods}.
   *
   * @return not null, vertx instance for the whole program.
   */
  @Bean public Vertx vertx() {
    return Vertx.currentContext().owner();
  }

  @Bean
  public EventBus eventBus() {
    return this.vertx().eventBus();
  }

}
