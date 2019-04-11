package es.msanchez.templates.complete.verticles;

import es.msanchez.templates.complete.addresses.EventBusAddress;
import io.vertx.core.eventbus.Message;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Component;

@Slf4j
@Component
public class HandlerExample extends AbstractVerticleHandler<String> {

  @Override protected String handleMessageBody(final Message<String> event) {
    log.info(String.format("FinalHandler called with parameter: %s", event.body()));
    return null;
  }

  @Override protected EventBusAddress eventBusAddress() {
    return EventBusAddress.MESSAGE_ADDRESS;
  }
}
