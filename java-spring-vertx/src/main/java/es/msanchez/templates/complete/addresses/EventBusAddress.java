package es.msanchez.templates.complete.addresses;

import lombok.AllArgsConstructor;

@AllArgsConstructor
public enum EventBusAddress {

  MESSAGE_ADDRESS("vertx.message.address");

  private String address;

  @Override
  public String toString() {
    return address;
  }
}
