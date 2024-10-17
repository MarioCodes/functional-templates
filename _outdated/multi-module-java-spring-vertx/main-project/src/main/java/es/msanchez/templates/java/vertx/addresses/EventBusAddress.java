package es.msanchez.templates.java.vertx.addresses;

import lombok.AllArgsConstructor;

@AllArgsConstructor
public enum EventBusAddress {

    MESSAGE_ADDRESS("vertx.message.address");

    private String address;

    @Override
    public String toString() {
        return this.address;
    }
}
