"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Contact = /** @class */ (function () {
    function Contact(data) {
        if (data) {
            this.id = data.id || void 0;
            this.firstName = data.firstName || void 0;
            this.lastName = data.lastName || void 0;
            this.companyName = data.companyName || void 0;
            this.address = data.address || void 0;
            this.city = data.city || void 0;
            this.state = data.state || void 0;
            this.post = data.post || void 0;
            this.phoneNumber1 = data.phoneNumber1 || void 0;
            this.phoneNumber2 = data.phoneNumber2 || void 0;
            this.email = data.email || void 0;
            this.webAddress = data.webAddress || void 0;
        }
    }
    return Contact;
}());
exports.Contact = Contact;
//# sourceMappingURL=contact.model.js.map