import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Col, Row } from 'react-bootstrap';

class Login extends Component {

    constructor(props) {
        super(props);
        this.state = {
            email: 'some.email@gmail.com',
            password: ''
        };
    }

    onLogin(email, password) {
        alert(email);
    }

    render() {

        return (
            <div>
                <h1>Login</h1>
                <Row>
                    <Col sm={3}>
                        <div className="form-group">
                            <label>Email</label>
                            <input type="email"
                                id="email"
                                className="form-control"
                                value={this.state.email} />
                        </div>
                    </Col>
                </Row>
                <Row>
                    <Col sm={3}>
                        <div className="form-group">
                            <label>Password</label>
                            <input type="password"
                                id="password"
                                className="form-control"
                                value={this.state.password} />
                        </div>
                    </Col>
                </Row>
                <button className="btn btn-default" onClick={() => this.onLogin(this.state.email, this.state.password)}>Login</button>
            </div>
        );
    }
}

export default connect()(Login);