package es.msanchez.templates.java.vertx;

import es.msanchez.templates.java.vertx.verticles.HandlerExample;
import io.vertx.core.Future;
import lombok.extern.slf4j.Slf4j;

@Slf4j
public class StarterVerticle extends AbstractStarterVerticle {

    @Override
    protected void startVerticleInstances() {
        this.deployVerticle(HandlerExample.class);
    }

    @Override
    public void stop(final Future<Void> stopFuture) {
        this.setApplicationContext(null);
        stopFuture.complete();
    }
}
