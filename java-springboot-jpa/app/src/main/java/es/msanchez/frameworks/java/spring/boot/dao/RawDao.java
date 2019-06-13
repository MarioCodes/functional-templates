package es.msanchez.frameworks.java.spring.boot.dao;

import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.NoRepositoryBean;

@NoRepositoryBean
interface RawDao<TYPE> extends CrudRepository<TYPE, Long> {
}
