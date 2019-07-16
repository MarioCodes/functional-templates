package es.msanchez.commons.java.builders;

import lombok.Getter;
import lombok.extern.slf4j.Slf4j;

import java.util.ArrayList;
import java.util.List;
import java.util.function.Consumer;

@Slf4j
@Getter
public abstract class AbstractBuilder<BUILDER, TYPE> extends Randomizer<TYPE> {

    private static final Integer DEFAULT_INSTANCES = 5;

    private final List<TYPE> instances;

    protected abstract TYPE instantiate();

    protected abstract BUILDER builder();

    protected AbstractBuilder() {
        this.instances = new ArrayList<>();
        this.prepareInstances();
    }

    private void prepareInstances() {
        for (int idx = 0; idx < DEFAULT_INSTANCES; idx++) {
            this.instances.add(this.instantiate());
        }
    }

    public TYPE build() {
        return this.instances.get(0);
    }

    public List<TYPE> buildList() {
        return this.instances;
    }

    public void with(final Consumer<TYPE> consumer) {
        this.instances.forEach(consumer);
    }

    public BUILDER random() {
        super.fill(this.instances);
        return this.builder();
    }

}
