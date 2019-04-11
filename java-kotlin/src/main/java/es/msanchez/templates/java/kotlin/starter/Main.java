package es.msanchez.templates.java.kotlin.starter;

import es.msanchez.templates.java.kotlin.beans.JavaBean;
import es.msanchez.templates.java.kotlin.beans.KotlinBean;
import lombok.extern.slf4j.Slf4j;

@Slf4j
public class Main {

  public static void main(String[] args) {
    log.info("Application started");

    final JavaBean javaBean = new JavaBean();
    log.info(javaBean.toString());

    final KotlinBean kotlinBean = new KotlinBean();
    log.info(kotlinBean.toString());

    log.info("Application is done");
  }

}
