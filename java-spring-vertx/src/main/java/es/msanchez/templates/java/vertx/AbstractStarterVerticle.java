package es.msanchez.templates.java.vertx;

import es.msanchez.templates.java.vertx.config.SpringConfig;
import es.msanchez.templates.java.vertx.utilities.SpringRegister;
import io.vertx.core.AbstractVerticle;
import io.vertx.core.DeploymentOptions;
import io.vertx.core.Verticle;
import io.vertx.core.Vertx;
import lombok.Getter;
import lombok.Setter;
import lombok.extern.slf4j.Slf4j;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

@Slf4j
@Getter
public abstract class AbstractStarterVerticle extends AbstractVerticle {

    @Setter
    private AnnotationConfigApplicationContext applicationContext;

    private SpringRegister register = new SpringRegister();

    /**
     * Way to deploy a standard Verticle once from {@link StarterVerticle}, giving only the class to deploy.
     *
     * @param clazz Class which extends from Verticle, that we want to deploy once.
     */
    protected void deployVerticle(final Class<? extends Verticle> clazz) {
        final DeploymentOptions options = new DeploymentOptions();
        final Verticle verticle = this.applicationContext.getBean(clazz);
        final Vertx vertx = this.applicationContext.getBean(Vertx.class);
        vertx.deployVerticle(verticle, options, result -> {
            if (result.succeeded())
                log.info("Deployment succeded!");
            else
                log.error("Error on deployment verticle: ", result.cause());
        });
    }

    @Override
    public void start() {
        this.applicationContext = this.register.initSpringApplicationContext(SpringConfig.class);
        this.startVerticleInstances();
    }

    protected abstract void startVerticleInstances();

}
