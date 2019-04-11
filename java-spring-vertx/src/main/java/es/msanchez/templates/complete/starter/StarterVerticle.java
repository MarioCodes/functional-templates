package es.msanchez.templates.complete.starter;

import es.msanchez.templates.complete.verticles.HandlerExample;
import io.vertx.core.Future;
import lombok.extern.slf4j.Slf4j;

/**
 * @author msanchez
 */
@Slf4j
public class StarterVerticle extends AbstractStarterVerticle {

	@Override
	protected void startVerticleInstances() {
		deployVerticle(HandlerExample.class);
	}

	@Override
	public void stop(final Future<Void> stopFuture) {
		setApplicationContext(null);
		stopFuture.complete();
	}
}
