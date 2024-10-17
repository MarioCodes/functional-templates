package es.msanchez.templates.java.springboot.jpa.dao;

import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.NoRepositoryBean;

@NoRepositoryBean
interface RawDao<TYPE> extends CrudRepository<TYPE, Long> {
}
