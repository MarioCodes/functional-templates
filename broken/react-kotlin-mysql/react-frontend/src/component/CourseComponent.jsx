import React from 'react';
import CourseDataService from "../service/CourseDataService";
import {Field, Form, Formik} from "formik";

class CourseComponent extends React.Component {

    constructor(props) {
        super(props)

        this.state = {
            id: this.props.match.params.id,
            description: ""
        }
    }

    componentDidMount() {
        console.log(this.state.id)

        // eslint-disable-next-line?
        if (this.state.id === -1) {
            return
        }

        CourseDataService.retrieveCourse(this.state.id)
            .then(response => this.setState(
                {description: response.data.description}
            ))
    }

    render() {
        let {description, id} = this.state
        return (
            <div>
                <h3>Course</h3>
                <div className="container">
                    <Formik
                        enableReinitialize={true}
                        initialValues={{
                            id: id,
                            description: description
                        }}>
                        {
                            (props) => (
                                <Form>
                                    <fieldset className="form-group">
                                        <label>Id</label>
                                        <Field className="form-control" type="text" name="id" disabled/>
                                    </fieldset>
                                    <fieldset className="form-group">
                                        <label>Description</label>
                                        <Field className="form-control" type="text" name="description"/>
                                    </fieldset>
                                    <button className="btn btn-success" type="submit">Save</button>
                                </Form>
                            )
                        }
                    </Formik>
                </div>
            </div>
        )
    }
}

export default CourseComponent
