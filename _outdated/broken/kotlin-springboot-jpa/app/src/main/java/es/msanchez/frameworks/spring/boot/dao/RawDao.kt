package es.msanchez.frameworks.spring.boot.dao

import org.springframework.data.repository.CrudRepository
import org.springframework.data.repository.NoRepositoryBean

@NoRepositoryBean
interface RawDao<TYPE> : CrudRepository<TYPE, Long>