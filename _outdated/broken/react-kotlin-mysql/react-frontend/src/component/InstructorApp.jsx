import React, {Component} from 'react';
import {BrowserRouter as Router, Route, Switch} from 'react-router-dom'
import ListCoursesComponent from './ListCoursesComponent'
import CourseComponent from './CourseComponent'

class InstructorApp extends Component {
    render() {
        return (
            <Router>
                <>
                    <h1>Instructor Application</h1>
                    <Switch>
                        <Route path="/" exact component={ListCoursesComponent}/>
                        <Route path="/courses" exact component={ListCoursesComponent}/>
                        <Route path="/courses/:id" exact component={CourseComponent}/>
                    </Switch>
                </>
            </Router>
        )
    }
}

export default InstructorApp
