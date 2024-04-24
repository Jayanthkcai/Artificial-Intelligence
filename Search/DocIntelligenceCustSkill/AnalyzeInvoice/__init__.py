import logging
import json
import os
import datetime
from json import JSONEncoder
import azure.functions as func
from azure.ai.formrecognizer import FormRecognizerClient
from azure.core.credentials import AzureKeyCredential


class DateTimeEncoder(JSONEncoder):
    # Override the default method
    def default(self, obj):
        if isinstance(obj, (datetime.date, datetime.datetime)):
            return obj.isoformat()


def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Invoked AnalyzeInvoice Skill.')
    try:
        body = req.get_json()
        if body:
            logging.info(body)
            result = compose_response(body)
            return func.HttpResponse(result, mimetype="application/json")
        else:
            return func.HttpResponse(
                "Invalid body",
                status_code=400
            )
    except ValueError:
        return func.HttpResponse(
            "Invalid body",
            status_code=400
        )


def compose_response(json_data):
    values = json_data.get('values', [])

    # Prepare the Output before the loop
    results = {"values": []}
    endpoint = os.environ["FORMS_RECOGNIZER_ENDPOINT"]
    key = os.environ["FORMS_RECOGNIZER_KEY"]
    form_recognizer_client = FormRecognizerClient(endpoint, AzureKeyCredential(key))
    for value in values:
        output_record = transform_value(value, form_recognizer_client)
        if output_record:
            results["values"].append(output_record)
    return json.dumps(results, ensure_ascii=False, cls=DateTimeEncoder)


## Perform an operation on a record
def transform_value(value, form_recognizer_client):
    try:
        recordId = value['recordId']
        data = value['data']
        form_url = data["formUrl"] + "?" + os.environ["FORM_SAS_Token"]
        poller = form_recognizer_client.begin_recognize_invoices_from_url(form_url)
        invoices = poller.result()
        invoiceResult = {}
        for invoice in invoices:
            for field_name, field in invoice.fields.items():
                if field:
                    invoiceResult[field_name] = field.value
    except KeyError as error:
        return {
            "recordId": recordId,
            "errors": [{"message": "Missing required field: {}".format(error)}]
        }
    except Exception as error:
        return {
            "recordId": recordId,
            "errors": [{"message": "Error: {}".format(error)}]
        }
    return {
        "recordId": recordId,
        "data": invoiceResult
    }
