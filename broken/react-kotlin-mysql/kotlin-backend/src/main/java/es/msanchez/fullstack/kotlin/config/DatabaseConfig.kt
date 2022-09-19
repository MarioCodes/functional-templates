package es.msanchez.fullstack.kotlin.config

import com.zaxxer.hikari.HikariConfig
import com.zaxxer.hikari.HikariDataSource
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.core.env.Environment
import org.springframework.data.jpa.repository.config.EnableJpaRepositories
import org.springframework.orm.jpa.JpaTransactionManager
import org.springframework.orm.jpa.LocalContainerEntityManagerFactoryBean
import org.springframework.orm.jpa.vendor.HibernateJpaVendorAdapter
import org.springframework.transaction.annotation.EnableTransactionManagement
import java.util.*
import javax.persistence.EntityManagerFactory
import javax.sql.DataSource

@Configuration
@EnableTransactionManagement
@EnableJpaRepositories(basePackages = ["es.msanchez.fullstack.kotlin.dao"])
class DatabaseConfig {

    @Bean
    fun dataSource(env: Environment): DataSource {
        var dataSourceConfig = HikariConfig()
        dataSourceConfig.driverClassName = env.getRequiredProperty("db.driver")
        dataSourceConfig.jdbcUrl = env.getRequiredProperty("db.url")
        dataSourceConfig.username = env.getRequiredProperty("db.username")
        dataSourceConfig.password = env.getRequiredProperty("db.password")
        return HikariDataSource(dataSourceConfig)
    }


    @Bean
    fun entityManagerFactory(dataSource: DataSource,
                                      env: Environment): LocalContainerEntityManagerFactoryBean {
        val entityManagerFactoryBean = LocalContainerEntityManagerFactoryBean()
        entityManagerFactoryBean.dataSource = dataSource
        entityManagerFactoryBean.jpaVendorAdapter = HibernateJpaVendorAdapter()
        entityManagerFactoryBean.setPackagesToScan("es.msanchez.fullstack.kotlin.**")

        val jpaProperties = Properties()

        //Configures the used database dialect. This allows Hibernate to create SQL
        //that is optimized for the used database.
        jpaProperties["hibernate.dialect"] = env.getRequiredProperty("hibernate.dialect")

        //Specifies the action that is invoked to the database when the Hibernate
        //SessionFactory is created or closed.
        jpaProperties["hibernate.hbm2ddl.auto"] = env.getRequiredProperty("hibernate.hbm2ddl.auto")

        //Configures the naming strategy that is used when Hibernate creates
        //new database objects and schema elements
        jpaProperties["hibernate.ejb.naming_strategy"] = env.getRequiredProperty("hibernate.ejb.naming_strategy")

        //If the value of this property is true, Hibernate writes all SQL
        //statements to the console.
        jpaProperties["hibernate.show_sql"] = env.getRequiredProperty("hibernate.show_sql")

        //If the value of this property is true, Hibernate will format the SQL
        //that is written to the console.
        jpaProperties["hibernate.format_sql"] = env.getRequiredProperty("hibernate.format_sql")

        entityManagerFactoryBean.setJpaProperties(jpaProperties)

        return entityManagerFactoryBean
    }

    @Bean
    fun transactionManager(entityManagerFactory: EntityManagerFactory): JpaTransactionManager {
        val transactionManager = JpaTransactionManager()
        transactionManager.entityManagerFactory = entityManagerFactory
        return transactionManager
    }

}