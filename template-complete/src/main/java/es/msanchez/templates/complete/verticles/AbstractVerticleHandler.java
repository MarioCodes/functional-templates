package es.msanchez.templates.complete.verticles;

import es.msanchez.templates.complete.addresses.EventBusAddress;
import io.vertx.core.AbstractVerticle;
import io.vertx.core.Handler;
import io.vertx.core.eventbus.Message;

/**
 * @author msanchez
 */
public abstract class AbstractVerticleHandler<R> extends AbstractVerticle
    implements Handler<Message<String>> {

  @Override
  public void handle(final Message<String> event) {
    R replyMessage = handleMessageBody(event);
    event.reply(replyMessage);
  }

  @Override
  public void start() {
    final EventBusAddress[] addresses = eventBusAddresses();
    for (EventBusAddress address : addresses)
      this.getVertx().eventBus().consumer(address.toString(), this);
  }

  protected abstract R handleMessageBody(final Message<String> body);

  protected abstract EventBusAddress eventBusAddress();

  protected EventBusAddress[] eventBusAddresses() {
    return new EventBusAddress[] {
        eventBusAddress() };
  }
}
