package es.msanchez.templates.java.vertx.verticles;

import es.msanchez.templates.java.vertx.addresses.EventBusAddress;
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
        final R replyMessage = this.handleMessageBody(event);
        event.reply(replyMessage);
    }

    @Override
    public void start() {
        final EventBusAddress[] addresses = this.eventBusAddresses();
        for (final EventBusAddress address : addresses)
            this.getVertx().eventBus().consumer(address.toString(), this);
    }

    protected abstract R handleMessageBody(final Message<String> body);

    protected abstract EventBusAddress eventBusAddress();

    protected EventBusAddress[] eventBusAddresses() {
        return new EventBusAddress[]{
                this.eventBusAddress()};
    }
}
