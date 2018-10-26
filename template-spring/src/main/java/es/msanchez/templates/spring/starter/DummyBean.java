package es.msanchez.templates.spring.starter;

import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Component;

/**
 * This is just a Bean usage example. Delete this and implement a real bean.
 */
@Slf4j @Component public class DummyBean {

  public DummyBean() {
    log.info("Bean injection was correctly called");
  }

}
